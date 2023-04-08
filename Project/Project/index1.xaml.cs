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
    /// Interaction logic for index1.xaml
    /// </summary>
    public partial class index1 : Window
    {
        public index1()
        {
            InitializeComponent();
            
        }

        private  void addItem(object sender, RoutedEventArgs e)
        {
            AddItemView window = new AddItemView();
            window.Show();
            this.Close();

        }
        private void editItems(object sender, RoutedEventArgs e)
        {
            EditItems editItems = new EditItems();
            editItems.Show();
            this.Close();
        }

        private void addCustomer(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
            this.Close();
        }
        private void goToCustomersList(object sender, RoutedEventArgs e)
        {
            CustomersList list = new CustomersList();
            this.Close();
            list.Show();
        }

    }
}
