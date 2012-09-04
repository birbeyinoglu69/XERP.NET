using System;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Windows.Threading;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Base class for non-detail view-models
    /// </summary>
    /// <typeparam name="TViewModel">Class inheriting from ViewModelBase</typeparam>
    public abstract class ViewModelBase<TViewModel> : ViewModelBase, INotifyPropertyChanged
    {
        // Dispatcher for cross-thread operations
        Dispatcher dispatcher = UIDispatcher.Current;

        #region Data Binding

        /// <summary>
        /// PropertyChanged event accessible to dervied classes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
        }
        protected PropertyChangedEventHandler propertyChanged;

        /// <summary>
        /// Allows you to specify a lambda for notify property changed
        /// </summary>
        /// <typeparam name="TResult">Property type</typeparam>
        /// <param name="property">Property for notification</param>
        protected virtual void NotifyPropertyChanged<TResult>
            (Expression<Func<TViewModel, TResult>> property)
        {
            // Fire PropertyChanged event
            BindingHelper.NotifyPropertyChanged(property, this, propertyChanged, dispatcher);
        }

        #endregion

        #region Notification Helpers

        /// <summary>
        /// Fire a notification event, which is transparently marshalled
        /// to the UI thread.
        /// </summary>
        /// <param name="handler">Notification event</param>
        /// <param name="e">Notification message</param>
        protected void Notify
            (EventHandler<NotificationEventArgs> handler,
            NotificationEventArgs e)
        {
            if (handler != null)
            {
                InternalNotify(() => handler(this, e));
            }
        }

        /// <summary>
        /// Fire a notification event, which is transparently marshalled
        /// to the UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Outgoing data type</typeparam>
        /// <param name="handler">Notification event</param>
        /// <param name="e">Notification message</param>
        protected void Notify<TOutgoing>
            (EventHandler<NotificationEventArgs<TOutgoing>> handler,
            NotificationEventArgs<TOutgoing> e)
        {
            if (handler != null)
            {
                InternalNotify(() => handler(this, e));
            }
        }

        /// <summary>
        /// Fire a notification event, which is transparently marshalled
        /// to the UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Outgoing data type</typeparam>
        /// <typeparam name="TIncoming">Incoming data type</typeparam>
        /// <param name="handler">Notification event</param>
        /// <param name="e">Notification message</param>
        protected void Notify<TOutgoing, TIncoming>
            (EventHandler<NotificationEventArgs<TOutgoing, TIncoming>> handler,
            NotificationEventArgs<TOutgoing, TIncoming> e)
        {
            if (handler != null)
            {
                InternalNotify(() => handler(this, e));
            }
        }

        private void InternalNotify(Action method)
        {
            // Always fire the event on the UI thread
            if (dispatcher.CheckAccess())
                method();
            else
                dispatcher.BeginInvoke(method);
        }

        #endregion

        #region Messaging Helpers

        // Proxy for communication with the MessageBus
        private MessageBusProxy messageBusHelper = new MessageBusProxy();

        /// <summary>
        /// Register callback using a string token, which is usually defined as a constant.
        /// </summary>
        /// <para>
        /// There is no need to unregister because receivers are allowed to be garbage collected.
        /// </para>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void RegisterToReceiveMessages(string token, EventHandler<NotificationEventArgs> callback)
        {
            // Register callback with MessageBusHelper and MessageBus
            this.messageBusHelper.Register(token, callback);
            MessageBus.Default.Register(token, this.messageBusHelper);
        }

        /// <summary>
        /// Register callback using string token and notification with TOutgoing data.
        /// </summary>
        /// <para>
        /// There is no need to unregister because receivers are allowed to be garbage collected.
        /// </para>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void RegisterToReceiveMessages<TOutgoing>(string token,
            EventHandler<NotificationEventArgs<TOutgoing>> callback)
        {
            // Register callback with MessageBusHelper and MessageBus
            this.messageBusHelper.Register(token, callback);
            MessageBus.Default.Register(token, this.messageBusHelper);
        }

        /// <summary>
        /// Register callback using string token and notification with TOutgoing data
        /// and the subscriber's callback with TIncoming data.
        /// </summary>
        /// <para>
        /// There is no need to unregister because receivers are allowed to be garbage collected.
        /// </para>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void RegisterToReceiveMessages<TOutgoing, TIncoming>(string token,
            EventHandler<NotificationEventArgs<TOutgoing, TIncoming>> callback)
        {
            // Register callback with MessageBusHelper and MessageBus
            this.messageBusHelper.Register(token, callback);
            MessageBus.Default.Register(token, this.messageBusHelper);
        }

        /// <summary>
        /// Unregister callback using a string token, which is usually defined as a constant.
        /// </summary>
        /// <para>
        /// This is optional because registered receivers are allowed to be garbage collected.
        /// </para>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void UnregisterToReceiveMessages(string token, EventHandler<NotificationEventArgs> callback)
        {
            // Unregister callback with MessageBusHelper and MessageBus
            this.messageBusHelper.Unregister(token, callback);
            MessageBus.Default.Unregister(token, this.messageBusHelper);
        }

        /// <summary>
        /// Unregister callback using string token and notification with TOutgoing data.
        /// </summary>
        /// <para>
        /// This is optional because registered receivers are allowed to be garbage collected.
        /// </para>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void UnregisterToReceiveMessages<TOutgoing>(string token,
            EventHandler<NotificationEventArgs<TOutgoing>> callback)
        {
            // Unregister callback with MessageBusHelper and MessageBus
            this.messageBusHelper.Unregister(token, callback);
            MessageBus.Default.Unregister(token, this.messageBusHelper);
        }

        /// <summary>
        /// Unregister callback using string token and notification with TOutgoing data
        /// and the subscriber's callback with TIncoming data.
        /// </summary>
        /// <para>
        /// This is optional because registered receivers are allowed to be garbage collected.
        /// </para>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        protected void UnregisterToReceiveMessages<TOutgoing, TIncoming>(string token,
            EventHandler<NotificationEventArgs<TOutgoing, TIncoming>> callback)
        {
            // Unregister callback with MessageBusHelper and MessageBus
            this.messageBusHelper.Unregister(token, callback);
            MessageBus.Default.Unregister(token, this.messageBusHelper);
        }

        /// <summary>
        /// Notify registered subscribers.
        /// Call is transparently marshalled to UI thread.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void SendMessage(string token, NotificationEventArgs e)
        {
            // Send notification through the MessageBus
            MessageBus.Default.Notify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers.
        /// Call is transparently marshalled to UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void SendMessage<TOutgoing>(string token, 
            NotificationEventArgs<TOutgoing> e)
        {
            // Send notification through the MessageBus
            MessageBus.Default.Notify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers.
        /// Call is transparently marshalled to UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void SendMessage<TOutgoing, TIncoming>(string token, 
            NotificationEventArgs<TOutgoing, TIncoming> e)
        {
            // Send notification through the MessageBus
            MessageBus.Default.Notify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers asynchronously.
        /// Call is not marshalled to UI thread.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void BeginSendMessage(string token, NotificationEventArgs e)
        {
            // Send notification through the MessageBus
            MessageBus.Default.BeginNotify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers asynchronously.
        /// Call is not marshalled to UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void BeginSendMessage<TOutgoing>(string token, 
            NotificationEventArgs<TOutgoing> e)
        {
            // Send notification through the MessageBus
            MessageBus.Default.BeginNotify(token, this, e);
        }

        /// <summary>
        /// Notify registered subscribers asynchronously.
        /// Call is not marshalled to UI thread.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="e">Event args carrying message</param>
        protected void BeginSendMessage<TOutgoing, TIncoming>(string token, 
            NotificationEventArgs<TOutgoing, TIncoming> e)
        {
            // Send notification through the MessageBus
            MessageBus.Default.BeginNotify(token, this, e);
        }

        #endregion
    }
}