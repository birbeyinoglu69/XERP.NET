using System.Windows;

namespace XERP.Client.WPF
{
    public class Utility
    {
        public bool SessionNotValidLogic()
        {
            string messageBoxText = "XERP Session Is Not Valid.  Log In Now?";
            string caption = "XERP Authentication Error";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;

                case MessageBoxResult.No:
                    return false;

                case MessageBoxResult.Cancel:
                    return false;

            }
            return false;
        }
    }
}
