using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;
using XERP.Client.WPF.StartUp.Services;
using XERP.Client;

namespace XERP.Client.WPF.StartUp.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class StartUpViewModel : ViewModelBase<StartUpViewModel>
    {
        #region Initialization and Cleanup

        // TODO: Add a member for IXxxServiceAgent
        private IStartUpServiceAgent _serviceAgent;

        // Default ctor
        public StartUpViewModel() { }

        // TODO: ctor that accepts IXxxServiceAgent
        public StartUpViewModel(IStartUpServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
        }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> LogInNotice;
        public event EventHandler<NotificationEventArgs<bool>> MainMenuNotice;

        #endregion

        #region Properties

        // TODO: Add properties using the mvvmprop code snippet

        #endregion

        #region Methods

        // TODO: Add methods that will be called by the view

        #endregion

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent
        
        #endregion

        #region Commands
        public void StartUpCommand()
        {
            
            RegisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
            NotifyLogIn();
        }

        private void OnStartUpLogIn(object sender, NotificationEventArgs<bool> e)
        {//if true is returned login was successful...
            NotifyMainMenu(e.Data);
            UnregisterToReceiveMessages<bool>(MessageTokens.StartUpLogInToken.ToString(), OnStartUpLogIn);
        }

        #endregion Commands

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
        //Notify view to launch login...
        private void NotifyLogIn()
        {
            Notify(LogInNotice, new NotificationEventArgs());
        }
        //Notify view to launch MainMenu...
        private void NotifyMainMenu(bool authenticated)
        {
            Notify(MainMenuNotice, new NotificationEventArgs<bool>("", authenticated));   
        }


        #endregion
    }
}