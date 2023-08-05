using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Configuration;
using System.Drawing;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string pathTologs = @"C:\Users\Public\Documents\Project.log";
        public bool showUpPass = false;
        public string selectedScannerKey;
        public string monikerKey;
        public static Scanner sensor;
        public static List<Customer> customerList;
        public static Dictionary<string, string> monikerScanners = new Dictionary<string, string>();
       
        public MainWindow()
        {
            InitializeComponent();
            createLogs();
            this.KeyDown += pressEnter;
            checkForPreviousSelections();

        }
        public void checkForPreviousSelections()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string scannerPref = config.AppSettings.Settings["Scanner"].Value;
            MainWindow.writeToLogs("Preferncess::::  " + scannerPref);
            if(!scannerPref.Equals(""))
            selectedScannerKey = scannerPref;
            string monikerPref = config.AppSettings.Settings["MonikerKey"].Value;
            MainWindow.writeToLogs("Preferncess::::  " + monikerPref);
            if (!monikerPref.Equals(""))
                monikerKey = monikerPref;
            if (!scannerPref.Equals("") && !monikerPref.Equals(""))
            {
                sensor = new Scanner(scannerPref,monikerPref,Scanner.PAGE.MAIN_MENUE);
            }
        }
        public static void createLogs()
        {
            if (!File.Exists(pathTologs))
                File.Create(pathTologs);
        }
        public static void writeToLogs(string s)
        {
            DateTime now = DateTime.Now;
            Thread.Sleep(100);
            try
            {
                File.AppendAllText(pathTologs, now.ToString() + "\n");
                File.AppendAllText(pathTologs, s + "\n");
            }catch(Exception e) {
                MainWindow.writeToLogs("Exception::::" + e.Message);
            }
        }

        private void pressEnter(object sender, KeyEventArgs e)
        {
            
            if(e.Key == Key.Enter)
            {
                _ = performLoginAsync();
            }
        }

        private void performLoginAsync(object sender, RoutedEventArgs e)
        {
            _ = performLoginAsync();
        }
        private void showPassFunc(object sender, RoutedEventArgs e)
        {
            if (user.Text.Equals("") && pass.Password.Equals(""))
            {
                return;
            }
            showUpPass = !showUpPass;
            if (showUpPass)
            {

                string value = pass.Password;
                showPass.Visibility = Visibility.Collapsed;
                hidePass.Visibility= Visibility.Visible;
                passVisible.Visibility = Visibility;
                pass.Visibility = Visibility.Hidden;
                passVisible.Text = value;
            }
            else
            {
                string value = passVisible.Text;
                hidePass.Visibility = Visibility.Collapsed;
                showPass.Visibility = Visibility.Visible; 
                pass.Visibility = Visibility.Visible;
                passVisible.Visibility = Visibility.Hidden;
                pass.Password = value;

            }
        }
        public async Task getCustomersAsync()
        {
            Task<List<Customer>> allItemsReq = RequestAsync.getCustomers();
            List<Customer> reqList = await allItemsReq;
            customerList=reqList.ToList();
            return;
        }


        private async Task performLoginAsync()
        {
            main.Effect= new BlurEffect();
            string userInput=user.Text.ToString();
            string userPass;
            if(!showUpPass)
            userPass = pass.Password.ToString();
            else
            userPass= passVisible.Text.ToString();

            if (userInput.Equals("") || userPass.Equals(""))
            {
                MessageBoxResult res = Popup.showMessage(Popup.Title.MISSING_ARGUMENTS);
                if (res == MessageBoxResult.OK)
                {
                    user.Text = "";
                    pass.Password = "";
                    main.Effect = null;
                }
                return;
            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
               
            }));
            Task<RequestAsync.Authentication> loginRequest = RequestAsync.loginRequestAsync(userInput, userPass);
            RequestAsync.Authentication loginSuccess = await loginRequest;
            bool succeeds = loginSuccess.Authenticated;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                
                spinner.Visibility = Visibility.Hidden;
                if (succeeds)
                {
                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => {
                        _ = getCustomersAsync();

                    }));
                    if (sensor != null)
                    {
                        Menue index1 = new Menue();
                        index1.Show();
                        this.Close();
                    }
                    else
                    {
                        Settings settings = new Settings();
                        settings.Show();
                        this.Close();
                    }
                    
                   
                }
                else
                {
                    MessageBoxResult res=Popup.showMessage(Popup.Title.LOGIN,loginSuccess.Message);
                    if (res==MessageBoxResult.OK)
                    {
                        if (loginSuccess.Message.Contains("Password"))
                        {
                            pass.Password = "";
                            main.Effect = null;
                        }
                        else
                        {
                            user.Text = "";
                            pass.Password = "";
                            main.Effect = null;
                        }
                        
                        
                    }
                }
                    
            })); 
        }


    }

}
