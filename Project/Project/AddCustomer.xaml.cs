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

namespace Project
{
    /// <summary>
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public int AddressId;
        public AddCustomer()
        {
            InitializeComponent();
        }
        private void saveItem(object sender, RoutedEventArgs e)
        {

            
            if (firstName.Text.ToString().Equals("") || lastName.ToString().Equals("") || nickName.ToString().Equals("") || telephone.ToString().Equals("") )

            {

                Popup.showMessage(Popup.Title.MISSING_ARGUMENTS);
                return;
            }
            if (AddressId == 0)
            {
                Popup.showMessage(Popup.Title.ADD_ADDRESS);
                return;
            }
            _ = saveCustomerAsync();
        }
        private async Task saveCustomerAsync()
        {

            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            string FirstName = firstName.Text;
            string LastName = lastName.Text;
            string NickName = nickName.Text;
            string tel =telephone.Text;
            Customer customer = new Customer(FirstName, LastName, NickName, tel, AddressId);
            Task<bool> newItemReq = RequestAsync.addCustomerAsync(customer);
            bool insert = await newItemReq;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Hidden;
                firstName.Text = "";
                lastName.Text = "";
                nickName.Text = "";
                telephone.Text = "";
            }));
        }
        private void backAction(object sender, RoutedEventArgs e)
        {
            index1 index1 = new index1();
            this.Close();
            index1.Show();
        }
        public void setAddrss(int address)
        {
            this.AddressId = address;
        }

 
        private void addAddress(object sender, RoutedEventArgs e)
        {
            this.Effect = new BlurEffect();
            var dialog = new AddAddress(this);
            dialog.ShowDialog();
        }
    }
}
