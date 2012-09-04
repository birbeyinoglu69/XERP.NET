using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Handles page navigation.
    /// </summary>
    public class Navigator : INavigator
    {
        /// <summary>
        /// Navigate to specified page.
        /// </summary>
        /// <param name="pageName">Name of page to navigate to</param>
        public void NavigateTo(string pageName)
        {
            Uri pageUri = new Uri("/" + pageName, UriKind.Relative);
            var frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            frame.Navigate(pageUri);
        }
    }
}