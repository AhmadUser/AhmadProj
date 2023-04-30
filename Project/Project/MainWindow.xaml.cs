
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string pathTologs = @"C:\Users\Public\Documents\Project.log";
        public bool showUpPass = false;
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(pressEnter);

        }
        public static void createLogs()
        {
            if (!File.Exists(pathTologs))
                File.Create(pathTologs);
        }
        public static void writeToLogs(string s)
        {
            DateTime now = DateTime.Now;
            File.AppendAllText(pathTologs, now.ToString() + "\n");
            File.AppendAllText(pathTologs, s + "\n");
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
            
            showUpPass = !showUpPass;
            if (showUpPass)
            {
                string value = pass.Password;
                passVisible.Visibility = Visibility.Visible;
                pass.Visibility = Visibility.Hidden;
                passVisible.Text = value;
            }
            else
            {
                string value = passVisible.Text;
                pass.Visibility = Visibility.Visible;
                passVisible.Visibility = Visibility.Hidden;
                pass.Password = value;

            }
        }
        
        private async Task performLoginAsync()
        {
            string userInput=user.Text.ToString();
            string userPass = pass.Password.ToString();
            if(userInput.Equals("") || userPass.Equals(""))
            {
                MessageBoxResult res = Popup.showMessage(Popup.Title.MISSING_ARGUMENTS);
                if (res == MessageBoxResult.OK)
                {
                    user.Text = "";
                    pass.Password = "";
                }
                return;
            }
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Visible;
            }));
            Task<bool> loginRequest = RequestAsync.loginRequestAsync(userInput, userPass);
            bool loginSuccess = await loginRequest;

            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                spinner.Visibility = Visibility.Hidden;
                if (loginSuccess)
                {
                    // index1 index1 = new index1();
                    //index1.Show();
                    Menue m = new Menue();
                    m.Show();
                    this.Close();
                }
                else
                {
                    MessageBoxResult res=Popup.showMessage(Popup.Title.LOGIN);
                    if (res==MessageBoxResult.OK)
                    {
                        user.Text = "";
                        pass.Password = "";
                    }
                }
                    
            })); 
        }
    }
 
    public class LoginResponse
    {
        public Authentication[] users;
    }
    public class Authentication
    {
        public bool Authenticated { get; set; }
    }
}
