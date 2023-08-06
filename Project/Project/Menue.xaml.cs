using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZXing.Datamatrix.Internal;
using ZXing.QrCode.Internal;

namespace Project
{
    /// <summary>
    /// Interaction logic for Menue.xaml
    /// </summary>
    public partial class Menue : Window
    {
        public static long barcodeToAdd = -1;
        public Thread thread;
        public static long prevBarcode = -1;
        public bool navi = false;
        public static List<Item> itemsScanned = new List<Item>();


        public ObservableCollection<Item> Products { get; set; }
        public long totalPrice;
        private bool isChecked = true;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }






        public long TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    totalPriceChanged(nameof(TotalPrice));
                }
            }
        }

        //// INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler totalPriceChangedProperty;


        protected virtual void totalPriceChanged(string propertyName)
        {
            totalPriceChangedProperty?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void navigation()
        {
            navi = true;
        }


        public Menue()
        {
            InitializeComponent();
            DataContext = this;
            TotalPrice = 0;
            Products = new ObservableCollection<Item>();
            Products.CollectionChanged += itemsListChanged;
            Closing += new CancelEventHandler(DataWindow_Closing);
            
            itemsScanned = new List<Item>();
            thread = new Thread(barcodeWatcher);
            thread.Start();
            if (MainWindow.sensor != null)
            {
                MainWindow.sensor.startScanner();
            }

        }

        private void itemsListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

                // Element(s) added to the collection
                foreach (var newItem in e.NewItems)
                {
                    Item curr = (Item)newItem;
                    itemsScanned.Add(curr);
                    totalPrice += curr.SellingPrice;
                    priceContainer.Text = totalPrice.ToString();
                    spinner.Visibility = Visibility.Hidden;
                    primary.Effect = null;
                    prevBarcode = barcodeToAdd;
                    barcodeToAdd = -1;

                }
            }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                //if (MainWindow.sensor != null)
                //    MainWindow.sensor.Dispose();
                //thread.Join();
                //while (thread.IsAlive) ;
                //Thread.Sleep(500);
            }));
        }

        private void goToAddItem(object sender, RoutedEventArgs e)
        {
            navigation();
            if (MainWindow.sensor != null)
            {
                MainWindow.sensor.Page = Scanner.PAGE.ADD_ITEM;
            }
            else
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    BlurEffect effect = new BlurEffect();
                    this.Effect = effect;
                }));
                MessageBoxResult result = Popup.showMessage(Popup.Title.MISSING_SENSOR);
                if (result == MessageBoxResult.OK)
                {
                    Settings settings = new Settings();

                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        settings.Show();
                        this.Close();
                    }));
                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        this.Effect = null;
                    }));

                }
                return;
            }

            AddItemView addItemView = new AddItemView();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                addItemView.Show();
                this.Close();
            }));

        }

        private void goToEdit(object sender, RoutedEventArgs e)
        {
            navigation();
            EditItems editItems = new EditItems();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                editItems.Show();
                this.Close();
            }));
        }

        private void goToSettings(object sender, RoutedEventArgs e)
        {
            navigation();
            Settings settings = new Settings();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                settings.Show();
                MainWindow.writeToLogs("Navigate");
                this.Close();
            }));
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!navi)
            {
                MainWindow.sensor.Dispose();
            }

        }

        private void barcodeWatcher()
        {
            while (MainWindow.sensor != null)
            {

                if (barcodeToAdd != -1)
                {
                    MainWindow.writeToLogs("okkk");
                    Console.Beep();

                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        primary.Effect = new BlurEffect();
                        spinner.Visibility = Visibility.Visible;
                    }));

                    _ = getItemAsync(barcodeToAdd);
                    Thread.Sleep(500);
                    prevBarcode = barcodeToAdd;
                    barcodeToAdd = -1;
                }

                Thread.Sleep(100); // Pause the thread for a short time to avoid excessive CPU usage
            }
        }

        private async Task getItemAsync(long barcode)
        {

            MainWindow.sensor.Pause = true;
            Task<Item> ItemRequest = RequestAsync.getItemByBarcodeAsync(barcode);
            Item item = await ItemRequest;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
             {
                 if (item != null)
                 {
                     if (item.Barcode == -404)
                     {
                         spinner.Visibility = Visibility.Hidden;
                         primary.Effect = null;
                         Popup.showMessage(Popup.Title.BARCODE);
                         barcodeToAdd = -1;
                         if (!thread.IsAlive)
                         {
                             //thread.Resume();
                         }
                     }
                     else
                     {
                         IsChecked = true;
                         item = item.selectItem();
                         Products.Add(item);
                     }

                 }
                 Thread.Sleep(500);

             }
            ));
        }

        private void createOrder(object sender, RoutedEventArgs e)
        {
            this.Effect = new BlurEffect();
            var dialog = new CreateOrder(itemsScanned, this);
            dialog.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                Products.Clear();
                TotalPrice = 0;
                priceContainer.Text = string.Empty;
            }));

        }

        private void TextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string barcode = barcodeManual.Text;
                barcodeToAdd = long.Parse(barcode);
                barcodeManual.Text = "";

            }
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Check if an item is selected and handle the double-click event here
            if (listItems.SelectedItem != null)
            {
                Item itemDoubleClicked = (Item)listItems.SelectedItem;
                Products.ElementAt<Item>(listItems.SelectedIndex).IsSelected = !itemDoubleClicked.IsSelected;
                totalPrice -= itemDoubleClicked.SellingPrice;
                priceContainer.Text = "" + totalPrice;
                isChecked = false;


            }
        }

        private void selectItem_Checked(object sender, RoutedEventArgs e)
        {
             
            var itemModel = (sender as CheckBox)?.DataContext as Item;

            if (itemModel != null)
            {
                modifyList(itemModel, true);
            }
        }
        private void unselectItem_Checked(object sender, RoutedEventArgs e)
        {

            var itemModel = (sender as CheckBox)?.DataContext as Item;

            if (itemModel != null)
            {
                modifyList(itemModel, false);
            }
        }
        private void modifyList(Item itemSelected,bool selected)
        {
            long newTotal = 0;

            int index=Products.IndexOf(itemSelected);
            Products.ElementAt<Item>(index).IsSelected = selected;
            for(int i = 0; i < Products.Count; i++)
            {
                if (Products.ElementAt<Item>(i).IsSelected)
                {
                    newTotal += Products.ElementAt<Item>(i).SellingPrice;
                }
            }
            totalPrice = newTotal;
            priceContainer.Text = "" + newTotal;
        }


    }

}

