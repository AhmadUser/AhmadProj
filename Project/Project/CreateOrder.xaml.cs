using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project
{
    /// <summary>
    /// Interaction logic for CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window, INotifyPropertyChanged
    {

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<string> Customers { get; set; }
        public Window parent { get; set; }
        public long  totalPrice { get; set; }
        public bool byButton { get; set; }  //total
        public CreateOrder(List<Item> items,Window p)
        {

            
            InitializeComponent();
            byButton = false;
            Items = new ObservableCollection<Item>();
            DataContext = this;
            Closing += CreateOrder_Closing;
            parent = p;
            totalPrice = 0;


            if (items != null)
            {
                itemsList.Visibility = Visibility.Visible;
                foreach (Item item in items)
                {
                    if(item.IsSelected)
                    Items.Add(item);
                    totalPrice += item.SellingPrice;
                }
            }
            fillList();
            

            

            //total.Text = " " + totalPrice;


        }
        public void fillList()  
        {
            Customers=new ObservableCollection<string>();
            foreach (Customer item in MainWindow.customerList)
            {
                MainWindow.writeToLogs(":::::::::::" + item.FirstName);

                    Customers.Add(item.FirstName);
            }
            customersList.ItemsSource = Customers;
            customersList.SelectionChanged += MyComboBox_SelectionChanged;





        }
        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Perform actions based on the selected item
            // e.g., retrieve the selected item: var selectedItem = myComboBox.SelectedItem;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateOrder_Closing(object sender, CancelEventArgs e)
        {
            if(!byButton)
            e.Cancel = true; // Cancel the closing event
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            byButton = true;
            //Menue.itemsScanned.Clear();
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                this.Close();
            }));
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                parent.Effect = null;
            }));

            

        }
    }
}
