using System;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Interface for navigation.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Navigate to specified page.
        /// </summary>
        /// <param name="pageName">Name of page to navigate to</param>
        void NavigateTo(string pageName);
    }
}
