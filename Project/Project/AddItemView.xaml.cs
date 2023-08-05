using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZXing;


namespace Project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddItemView : Window
    {
        public static long barcodeToAdd = -1;
        public Thread thread;
        public long prevBarcode = -1;

        public AddItemView()
        {
            InitializeComponent();
            thread = new Thread(barcodeWatcher);
            thread.Start();
            if (MainWindow.sensor != null)
                MainWindow.sensor.startScanner();
            Closing += new CancelEventHandler(DataWindow_Closing);
        }

        //public void startScanner(object sender, RoutedEventArgs e)
        //{
        //    scanner.startScanner();
        //}


        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                //if (MainWindow.sensor != null)
                //    MainWindow.sensor.Dispose();
                thread.Abort();
                while (thread.IsAlive) ;
                //Thread.Sleep(500); 
            }));


        }

        private void backAction(object sender, RoutedEventArgs e)
        {
            Menue index1 = new Menue();
            MainWindow.sensor.Page = Scanner.PAGE.MAIN_MENUE;
            index1.Show();
            this.Close();

        }
        private void saveItem(object sender, RoutedEventArgs e)
        {
            if (barcode.Text.ToString().Equals("") || quantity.ToString().Equals("") || initialPrice.ToString().Equals("") || sellingPrice.ToString().Equals("") || companyName.ToString().Equals("") || privateName.ToString().Equals(""))

            {
                System.Windows.MessageBox.Show("Make sure to fill all the fields");
                return;
            }
            _ = saveItemAsync();
        }
        private async Task saveItemAsync()
        {
            long initPrice = 0;
            long sellPrice = 0;
            long bar = 0;
            long quant = 0;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            Int64.TryParse(initialPrice.Text, out initPrice);
            Int64.TryParse(sellingPrice.Text, out sellPrice);
            bar = long.Parse(barcode.Text);
            Int64.TryParse(quantity.Text, out quant);
            string comName = companyName.Text;
            string privName = privateName.Text;
            Task<bool> newItemReq = RequestAsync.newItemAsync(new Item((int)initPrice, (int)sellPrice, comName, privName, bar, quant.ToString()));
            bool insert = await newItemReq;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Hidden;
                initialPrice.Text = "";
                sellingPrice.Text = "";
                barcode.Text = "";
                quantity.Text = "";
                companyName.Text = "";
                privateName.Text = "";
            }));
        }
        private void barcodeWatcher()
        {
            while (MainWindow.sensor != null && !MainWindow.sensor.Pause)
            {



                if (barcodeToAdd != -1 && (prevBarcode==-1 || barcodeToAdd!=prevBarcode))
                {
                    Console.Beep();
                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        barcode.Text = "" + barcodeToAdd;

                    }));
                    Thread.Sleep(100);
                    prevBarcode=barcodeToAdd;
                    barcodeToAdd = -1;

                }
            }


        }
    }

}
