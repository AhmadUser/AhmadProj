using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project
{
    /// <summary>
    /// Interaction logic for EditItems.xaml
    /// </summary>
    public partial class EditItems : Window
    {
        public static List<Item> items=new List<Item>();
        public EditItems()
        {
            InitializeComponent();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            _ = getItemsAsync();
        }
        private async Task getItemsAsync()
        {
            Task<List<Item>> allItemsReq = RequestAsync.getItems();
             List<Item> reqList = await allItemsReq;
            items.AddRange(reqList);
            if (items != null )
            {
                listItems.Visibility = Visibility.Visible;
                backButton.IsEnabled = true;
               foreach (Item item in items)
               {
   
                    listItems.Items.Add(item.PrivateName);
               }
            }
            else
            {
                MessageBox.Show("Error while fetching items");
            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Hidden;
            }));
        }
        public void onSelectedItem(object sender, RoutedEventArgs e)
        {

            Item newItem = (Item)items.ElementAt<Item>(listItems.SelectedIndex);
            newItem.toString();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                try
                {
                    InitialPrice.Text = "" + newItem.InitialPrice;
                    InitialPrice.Visibility = Visibility.Visible;
                    InitialPriceLabel.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MainWindow.writeToLogs("1");
                }

                try
                {
                    SellingPrice.Text = "" + newItem.SellingPrice;
                    SellingPrice.Visibility = Visibility.Visible;
                    SellingPriceLabel.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MainWindow.writeToLogs("11");
                    // Handle or log the exception
                }

                try
                {
                    CompanyName.Text = "" + newItem.CompanyName;
                    CompanyName.Visibility = Visibility.Visible;
                    CompanyNameLabel.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    MainWindow.writeToLogs("111");
                }

                try
                {
                    PrivateName.Text = "" + newItem.PrivateName;
                    PrivateName.Visibility = Visibility.Visible;
                    PrivateNameLabel.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    MainWindow.writeToLogs("1111");
                }

                try
                {
                    Barcode.Text = "" + newItem.Barcode;
                    Barcode.Visibility = Visibility.Visible;
                    BarcodeLabel.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    MainWindow.writeToLogs("11111");
                }

                try
                {
                    Quantity.Text = "" + newItem.Quantity;
                    Quantity.Visibility = Visibility.Visible;
                    QuantityLabel.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    MainWindow.writeToLogs("111111");
                }

            }));


        }

        private void backAction(object sender, RoutedEventArgs e)
        {
            
            Menue index1 = new Menue();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                index1.Show();
                this.Close();
            }));

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            Item edited=new Item();
            edited.PrivateName=PrivateName.Text;
            edited.CompanyName=CompanyName.Text;
            edited.InitialPrice = int.Parse(InitialPrice.Text);
            edited.SellingPrice = int.Parse(SellingPrice.Text);
            edited.Barcode=long.Parse(Barcode.Text);
            edited.Quantity = Quantity.Text;
            _ = editItemsAsync(edited);
        }
        private async Task editItemsAsync(Item item)
        {
            Task<bool> allItemsReq = RequestAsync.editItemsAsync(item);
            bool reqList = await allItemsReq;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Hidden;
               
            }));
        }
    }
}
