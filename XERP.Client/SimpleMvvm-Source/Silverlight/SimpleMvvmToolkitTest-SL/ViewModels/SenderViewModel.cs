using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace SimpleMvvmToolkitTest
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class SenderViewModel : ViewModelBase<SenderViewModel>
    {
        #region Initialization and Cleanup

        // Default ctor
        public SenderViewModel() { }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        // Message values
        public string SentMessage { get; set; }
        public int SentData { get; set; }
        public bool ReceivedData { get; set; }

        #endregion

        #region Methods

        // Send message via the message bus
        public void SendSimpleMessage()
        {
            SendMessage(MessageTokens.Test, new NotificationEventArgs(SentMessage));
        }

        // Send message via the message bus
        public void SendOneWayMessage()
        {
            SendMessage(MessageTokens.Test, new NotificationEventArgs<int>
                (SentMessage, SentData));
        }

        // Send message via the message bus
        public void SendTwoWayMessage()
        {
            SendMessage(MessageTokens.Test, new NotificationEventArgs<int, bool>
                (SentMessage, SentData, val => ReceivedData = val));
        }

        #endregion

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

        #endregion
    }
}