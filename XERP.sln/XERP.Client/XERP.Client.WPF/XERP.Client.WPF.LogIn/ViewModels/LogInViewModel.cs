using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

//XERP Namespaces
using XERP.Domain.LogInDomain.Services;

namespace XERP.Client.WPF.LogIn.ViewModels
{
    public class LogInViewModel : ViewModelBase<LogInViewModel>
    {
        #region Initialization and Cleanup
        private ILogInServiceAgent _serviceAgent;

        // TODO: ctor that accepts IXxxServiceAgent
        public LogInViewModel(ILogInServiceAgent serviceAgent)
        {
            this._serviceAgent = serviceAgent;
        }

        #endregion

        #region Notifications
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs> MessageNotice;
        public event EventHandler<NotificationEventArgs> LogInActionNotice;
        #endregion

        #region Properties
        private string _userNameInput;
        public string UserNameInput
        {
            get { return _userNameInput; }
            set
            {
                _userNameInput = value;
                NotifyPropertyChanged(m => m.UserNameInput);
            }
        }

        private string _passwordInput;
        public string PasswordInput
        {
            get { return _passwordInput; }
            set
            {
                _passwordInput = value;
                NotifyPropertyChanged(m => m.PasswordInput);
            }
        }
        #endregion

        #region Methods
        private bool Authenticated(string systemUserID, string password, out string authenticationMessage)
        {
            return _serviceAgent.Authenticated(systemUserID, password, out authenticationMessage);
        }
        #endregion Methods

        #region Commands
        public void AuthenticateCommand()
        {
            string authenticationMessage = "";
            if (Authenticated(UserNameInput, PasswordInput, out authenticationMessage))
            {
                MessageBus.Default.Notify("StartUpLogInToken", this, new NotificationEventArgs<bool>("", true)); 
            }
            else
            {
                NotifyMessage(authenticationMessage);
            }
        }

        #endregion Commands

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent
        
        #endregion

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
            
        }
        private void NotifyMessage(string message)
        {
            Notify(MessageNotice, new NotificationEventArgs(message));
        }

        private void NotifyLogInAction(string message)
        {
            Notify(LogInActionNotice, new NotificationEventArgs(message));
        }
        #endregion
    }
}