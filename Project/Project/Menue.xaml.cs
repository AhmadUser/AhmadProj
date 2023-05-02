using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project
{
    /// <summary>
    /// Interaction logic for Menue.xaml
    /// </summary>
    public partial class Menue : Window
    {
        public bool isRunning = false;
        public static string barcode = null;
        Thread barcodeChecker;
        public Menue()
        {
            InitializeComponent();
            barcodeChecker = new Thread(barcodeCheckerWorker);
            barcodeChecker.Start();
        }

        private void goToAddItem(object sender, RoutedEventArgs e)
        {
            AddItemView addItemView = new AddItemView();
            addItemView.Show();
            this.Close();
        }

        private void fireCamera(object sender, RoutedEventArgs e)
        {
           // Scanner s = new Scanner();
        }
        public void   barcodeCheckerWorker()
        {
            while (isRunning)
            {
                if (barcode != null)
                {
                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                    Console.Beep(37, 1000);
                    MessageBox.Show(barcode);
                     barcode=null;

                    }));
                }
            }
        }
    }
}
