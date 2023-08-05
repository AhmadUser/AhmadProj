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

namespace Project
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList : Window
    {
        public static List<Customer> customers = new List<Customer>();
        public CustomersList()
        {
            InitializeComponent();
            _ = getCustomersAsync();
        }
        public async Task getCustomersAsync()
        {
            Task<List<Customer>> allItemsReq = RequestAsync.getCustomers();
            List<Customer> reqList = await allItemsReq;
            customers.AddRange(reqList);
            if (customers != null)
            {
                listCustomers.Visibility = Visibility.Visible;
                // backButton.IsEnabled = true;
                foreach (Customer customer in customers)
                {
                    string tempName = customer.FirstName + " " + customer.LastName + "(" + customer.NickName + ")";
                    listCustomers.Items.Add(tempName);
                }
            }
            else
            {
                MessageBox.Show("Error while fetching items");
            }
            /* this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
             {
                 spinner.Visibility = Visibility.Hidden;
             }));*/
        }

        private void onSelectedItem(object sender, MouseButtonEventArgs e)
        {
            Customer selectedCustomer = new Customer();
            //CreateOrder createOrder = new CreateOrder();
            //createOrder.Show();
            this.Close();
        }
    }
}
