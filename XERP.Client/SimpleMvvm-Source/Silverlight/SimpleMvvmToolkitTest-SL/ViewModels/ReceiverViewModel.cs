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
    public class ReceiverViewModel : ViewModelBase<ReceiverViewModel>
    {
        #region Initialization and Cleanup

        // Default ctor
        public ReceiverViewModel() { }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        // Message values
        public string ReceivedMessage { get; set; }
        public int ReceivedData { get; set; }
        public bool SentData { get; set; }

        #endregion

        #region Methods

        public void Subscribe()
        {
            // Register receive methods
            RegisterToReceiveMessages(MessageTokens.Test, ReceiveSimpleMessage);
            RegisterToReceiveMessages<int>(MessageTokens.Test, ReceiveOneWayMessage);
            RegisterToReceiveMessages<int, bool>(MessageTokens.Test, ReceiveTwoWayMessage);
        }

        public void Unsubscribe()
        {
            // Unregister receive methods
            UnregisterToReceiveMessages(MessageTokens.Test, ReceiveSimpleMessage);
            UnregisterToReceiveMessages<int>(MessageTokens.Test, ReceiveOneWayMessage);
            UnregisterToReceiveMessages<int, bool>(MessageTokens.Test, ReceiveTwoWayMessage);
        }

        // Receive methods
        private void ReceiveSimpleMessage(object sender, NotificationEventArgs e)
        {
            ReceivedMessage = e.Message;
        }

        private void ReceiveOneWayMessage(object sender, NotificationEventArgs<int> e)
        {
            ReceivedData = e.Data;
        }

        private void ReceiveTwoWayMessage(object sender, NotificationEventArgs<int, bool> e)
        {
            ReceivedData = e.Data;
            e.Completed(SentData);
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