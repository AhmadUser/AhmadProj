using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Project
{
    public class Popup
    {
        public static System.Windows.MessageBoxResult showMessage(Title title)
        {
            string message, mainMessage;
            MessageBoxImage messageBoxImage;
            if (title.Equals(Title.LOGIN))
            {
                message = "Invalid credentials,Please try again";
                mainMessage = "Login Failure";
                messageBoxImage = MessageBoxImage.Error;
            }
            else if (title.Equals(Title.MISSING_ARGUMENTS))
            {
                message = "Fill out all the fields";
                mainMessage = "Missed Arguments";
                messageBoxImage = MessageBoxImage.Exclamation;
            }
            else if (title.Equals(Title.NUMBER))
            {
                message = "Value must be a number";
                mainMessage = "UnAcceptable input";
                messageBoxImage = MessageBoxImage.Exclamation;
            }
            else if (title.Equals(Title.MISSING_SENSOR))
            {
                message = "You need to select Scanner to continue";
                mainMessage = "No Scanner Detected";
                messageBoxImage = MessageBoxImage.Exclamation;
            }
            else if (title.Equals(Title.BARCODE))
            {
                message = "Recapture please";
                mainMessage = "Error("+Menue.prevBarcode+")";
                messageBoxImage = MessageBoxImage.Information;
            }
            else
            {
                message = "Default";
                mainMessage = "DefaultMain";
                messageBoxImage = MessageBoxImage.None;
            }
            MessageBoxResult res=MessageBox.Show(message,mainMessage,MessageBoxButton.OK, messageBoxImage);
            return res;
        }
        public static System.Windows.MessageBoxResult showMessage(Title title,string message)
        {
            string  mainMessage;
            MessageBoxImage messageBoxImage;
            if (title.Equals(Title.LOGIN))
            {

                mainMessage = "Login Failure";
                messageBoxImage = MessageBoxImage.Error;
            }
            else if (title.Equals(Title.MISSING_ARGUMENTS))
            {

                mainMessage = "Missed Arguments";
                messageBoxImage = MessageBoxImage.Exclamation;
            }
            else if (title.Equals(Title.NUMBER))
            {
                
                mainMessage = "UnAcceptable input";
                messageBoxImage = MessageBoxImage.Exclamation;
            }
            else if (title.Equals(Title.MISSING_SENSOR))
            {
                
                mainMessage = "No Scanner Detected";
                messageBoxImage = MessageBoxImage.Exclamation;
            }
            else if (title.Equals(Title.BARCODE))
            {
                
                mainMessage = "Error(" + Menue.prevBarcode + ")";
                messageBoxImage = MessageBoxImage.Information;
            }
            else
            {
                
                mainMessage = "DefaultMain";
                messageBoxImage = MessageBoxImage.None;
            }
            MessageBoxResult res = MessageBox.Show(message, mainMessage, MessageBoxButton.OK, messageBoxImage);
            return res;
        }
        public  enum Title
        {
            LOGIN,
            ADD_ADDRESS,
            ADD_CUSTOMER,
            ADD_ITEM,
            NUMBER,
            MISSING_ARGUMENTS,
            MISSING_SENSOR,
            BARCODE
        }
    }


}
