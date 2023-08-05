using AForge.Video.DirectShow;
using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        FilterInfoCollection filterInfoCollection;
        string selectedMoniker;
        string selectedContent;
        public Settings()
        {
            //Closing += new CancelEventHandler(DataWindow_Closing);
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
                    MainWindow.monikerScanners.Add(device.Name, device.MonikerString);
                    MainWindow.writeToLogs(device.Name);
                }
            }
            catch (Exception ex)
            {
                MainWindow.writeToLogs(ex.ToString());
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.AppSettings.Settings.Clear();
            config.AppSettings.Settings["Scanner"].Value = selectedContent;
            config.AppSettings.Settings["MonikerKey"].Value = selectedMoniker;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            MainWindow.sensor = new Scanner(selectedContent, selectedMoniker,Scanner.PAGE.MAIN_MENUE);
            Menue menu = new Menue();
            menu.Show();
            this.Close();
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedContent = listOfCams.SelectedItem.ToString();
            selectedMoniker = "";
            MainWindow.monikerScanners.TryGetValue(selectedContent, out selectedMoniker);
            MainWindow.writeToLogs("Testinggggg:::  " + selectedContent);

        }

        /*void DataWindow_Closing(object sender, CancelEventArgs e)
{
   if (videoCaptureDevice != null)
   {
       if (videoCaptureDevice.IsRunning)
       {
           videoCaptureDevice.Stop();
       }
   }
}*/
    }
}
