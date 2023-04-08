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
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                InitialPrice.Text = "" +newItem.InitialPrice;
                InitialPrice.Visibility = Visibility.Visible;
                InitialPriceLabel.Visibility = Visibility.Visible;
                SellingPrice.Text = "" + newItem.SellingPrice;
                SellingPrice.Visibility = Visibility.Visible;
                SellingPriceLabel.Visibility = Visibility.Visible;
                CompanyName.Text = "" + newItem.CompanyName;
                CompanyName.Visibility = Visibility.Visible;
                CompanyNameLabel.Visibility = Visibility.Visible;
                PrivateName.Text = "" + newItem.PrivateName;
                PrivateName.Visibility = Visibility.Visible;
                PrivateNameLabel.Visibility = Visibility.Visible;
                Barcode.Text = "" + newItem.Barcode;
                Barcode.Visibility = Visibility.Visible;
                BarcodeLabel.Visibility = Visibility.Visible;
                Quantity.Text = "" + newItem.Quantity;
                Quantity.Visibility = Visibility.Visible;
                QuantityLabel.Visibility = Visibility.Visible;
            }));


        }

        private void backAction(object sender, RoutedEventArgs e)
        {
            
            index1 index1 = new index1();
            this.Close();
            index1.Show();
        }
    }
}
