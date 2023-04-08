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
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public AddItemView()
        {
            Closing += new CancelEventHandler(DataWindow_Closing);
            InitializeComponent();
            searchForScanners();
        }

        public void searchForScanners()
        {
            try
            {
                filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo device in filterInfoCollection)
                {
                    listOfCams.Items.Add(device.Name);
                    MainWindow.writeToLogs(device.Name);
                }
            }
            catch(Exception ex)
            {
                MainWindow.writeToLogs(ex.ToString());
            }

        }
        public void startScanner(object sender, RoutedEventArgs e)
        {
            try { 
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[listOfCams.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += NewFrame;
            //videoCaptureDevice.NewFrame += null;
            videoCaptureDevice.Start();
            }catch(Exception ex)
            {
                MainWindow.writeToLogs(ex.ToString());
            }
        }

        private void NewFrame(object sender, NewFrameEventArgs eventArgs)
        {

            MainWindow.writeToLogs("Thread running ");
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader barcodeReader = new BarcodeReader();
            var result = barcodeReader.Decode(bitmap);
              if (result != null)
              {
                  MainWindow.writeToLogs("Barcode " + result.Text);
                  Console.Beep();
                  this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                  {
                      Console.Beep(37,1000);
                      barcode.Text = result.Text;

                  }));


              }


        }
        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            if (videoCaptureDevice!=null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.Stop();
                }
            }

        }
        private void backAction(object sender, RoutedEventArgs e)
        {
            index1 index1 = new index1();
            this.Close();
            index1.Show();
        }
        private void saveItem(object sender, RoutedEventArgs e)
        {
            if (barcode.Text.ToString().Equals("") || qunatity.ToString().Equals("")  || initialPrice.ToString().Equals("")  || sellingPrice.ToString().Equals("") || companyName.ToString().Equals("") || privateName.ToString().Equals(""))

            {
                System.Windows.MessageBox.Show("Make sure to fill all the fields");
                return;
            }
            _ = saveItemAsync();
        }
        private async Task saveItemAsync()
        {
            long initPrice=0;
            long sellPrice=0;
            long bar=0;
            long quant = 0;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            Int64.TryParse(initialPrice.Text,out initPrice);
            Int64.TryParse(sellingPrice.Text, out sellPrice);
            Int64.TryParse(barcode.Text, out bar);
            Int64.TryParse(qunatity.Text, out quant);
            string comName = companyName.Text;
            string privName = privateName.Text;
            Task<bool> newItemReq = RequestAsync.newItemAsync(new Item((int)initPrice,(int)sellPrice,comName,privName,(int)bar,quant.ToString()));
            bool insert = await newItemReq;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Hidden;
                initialPrice.Text = "";
                sellingPrice.Text = "";
                barcode.Text = "";
                qunatity.Text = "";
                companyName.Text="";
                privateName.Text="";
            }));
        }
    }
    
}
