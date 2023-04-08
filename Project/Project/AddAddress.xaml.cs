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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace Project
{
    /// <summary>
    /// Interaction logic for AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Window
    {
        public AddCustomer ParentWindow;
        public AddAddress(AddCustomer parent)
        {
            ParentWindow = parent;
            InitializeComponent();
        }
        private void saveAddress(object sender, RoutedEventArgs args)
        {
            if (streetName.Text.ToString().Equals("") || buildingName.ToString().Equals("") || floor.ToString().Equals("") || near.ToString().Equals("") || details.ToString().Equals("") )

            {
                MessageBoxResult res = Popup.showMessage(Popup.Title.MISSING_ARGUMENTS);
                return;
            }
            _=saveAddressAsync();

        }
        
     private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
     {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                Popup.showMessage(Popup.Title.NUMBER);
            }));
        }

    private async Task saveAddressAsync()
        {
            string street = streetName.Text.ToString();
            string building = buildingName.Text.ToString();
            int Customerfloor = Int32.Parse(floor.Text);
            string nearPlace = near.Text.ToString();
            string Moredetails = details.Text.ToString();

            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            Task<int> newItemReq = RequestAsync.addAddressAsync(new Address(street, building, Customerfloor, nearPlace, Moredetails));
            int AddressId = await newItemReq;

            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                MainWindow.writeToLogs("Id:::::"+AddressId);
                spinner.Visibility = Visibility.Hidden;
                ParentWindow.setAddrss(AddressId);
                this.Close();
                ParentWindow.Effect = null;
            }));
        }


    }
}
