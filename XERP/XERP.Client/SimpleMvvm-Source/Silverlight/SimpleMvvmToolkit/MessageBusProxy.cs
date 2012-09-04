using System;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Handles communication with the MessageBus.
    /// ViewModelBase stores this internally.
    /// </summary>
    internal class MessageBusProxy : INotifyable
    {
        // Provide thread-safe access to subscriptions
        private object sharedLock = new object();

        // Each token can have multiple callbacks
        private Dictionary<string, List<Handler>> subscriptions =
            new Dictionary<string, List<Handler>>();

        #region Registration

        /// <summary>
        /// Register callback using a string token, which is usually defined as a constant
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        public void Register(string token, EventHandler<NotificationEventArgs> callback)
        {
            InternalRegister(token, callback, typeof(NotificationEventArgs));
        }

        /// <summary>
        /// Register callback using string token and notification with TOutgoing data
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        public void Register<TOutgoing>(string token,
            EventHandler<NotificationEventArgs<TOutgoing>> callback)
        {
            InternalRegister(token, callback, typeof(NotificationEventArgs<TOutgoing>));
        }

        /// <summary>
        /// Register callback using string token and notification with TOutgoing data
        /// and the subscriber's callback with TIncoming data.
        /// </summary>
        /// <typeparam name="TOutgoing">Type used by notifier to send data</typeparam>
        /// <typeparam name="TIncoming">Type sent by subscriber to send data back to notifier</typeparam>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to invoke when notified</param>
        public void Register<TOutgoing, TIncoming>(string token,
            EventHandler<NotificationEventArgs<TOutgoing, TIncoming>> callback)
        {
            InternalRegister(token, callback, typeof(NotificationEventArgs<TOutgoing, TIncoming>));
        }

        /// <summary>
        /// Remove callback from the invocation list.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to remove from notifications</param>
        public void Unregister(string token, EventHandler<NotificationEventArgs> callback)
        {
            InternalUnregister(token, callback, typeof(NotificationEventArgs));
        }

        /// <summary>
        /// Remove callback from the invocation list.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to remove from notifications</param>
        public void Unregister<TOutgoing>(string token,
            EventHandler<NotificationEventArgs<TOutgoing>> callback)
        {
            InternalUnregister(token, callback, typeof(NotificationEventArgs<TOutgoing>));
        }

        /// <summary>
        /// Remove callback from the invocation list.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="callback">Method to remove from notifications</param>
        public void Unregister<TOutgoing, TIncoming>(string token,
            EventHandler<NotificationEventArgs<TOutgoing, TIncoming>> callback)
        {
            InternalUnregister(token, callback, typeof(NotificationEventArgs<TOutgoing, TIncoming>));
        }

        private void InternalUnregister(string token, Delegate callback, Type eventArgsType)
        {
            lock (sharedLock)
            {
                if (subscriptions.ContainsKey(token))
                {
                    // Get handler with the same type
                    Handler handler = subscriptions[token]
                        .Where(h => h.EventArgsType == eventArgsType)
                        .SingleOrDefault();

                    // If registered, remove callback from the list
                    if (handler != null) subscriptions[token].Remove(handler);

                    // Remove dictionary entry if no subscibers left
                    if (subscriptions[token].Count == 0) subscriptions.Remove(token);
                }
            }
        }

        private void InternalRegister(string token, Delegate callback, Type eventArgsType)
        {
            // If already registered, add callback to the list
            lock (sharedLock)
            {
                if (subscriptions.ContainsKey(token))
                {
                    subscriptions[token].Add(new Handler(callback, eventArgsType));
                }
                // Otherwise create an entry with a new callback list
                else
                {
                    subscriptions.Add(token, new List<Handler> 
                        { new Handler(callback, eventArgsType) });
                } 
            }
        }

        #endregion

        #region Notification

        /// <summary>
        /// Notify registered callbacks.
        /// </summary>
        /// <param name="token">String identifying a message token</param>
        /// <param name="sender">Sender of notification</param>
        /// <param name="e">Event args carrying message</param>
        void INotifyable.Notify(string token, object sender, NotificationEventArgs e)
        {
            // Notify subscriber on UI thread
            InternalNotify(token, sender, e);
        }

        private void InternalNotify(string token, object sender,
            NotificationEventArgs e)
        {
            List<Handler> handlers;
            lock (sharedLock)
            {
                // Return if no handlers registered
                if (!subscriptions.TryGetValue(token, out handlers)) return;

                // Get callbacks with the same event args type
                handlers = handlers
                    .Where(h => h.EventArgsType == e.GetType())
                    .ToList();
            }

            // Invoke each callback associated with token
            foreach (var handler in handlers)
            {
                if (handler != null)
                {
                    handler.Callback.DynamicInvoke(sender, e);
                }
            }
        }

        #endregion

        #region Helper Classes

        internal class Handler
        {
            public Delegate Callback { get; set; }
            public Type EventArgsType { get; set; }

            public Handler(Delegate callback, Type eventArgsType)
            {
                this.Callback = callback;
                this.EventArgsType = eventArgsType;
            }
        }

        #endregion
    }
}
