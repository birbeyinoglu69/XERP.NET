using System;
using System.Windows;
using System.Windows.Threading;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Helper class for dispatching work across threads.
    /// WPF apps should call Initialize from the UI thread in App_Start.
    /// </summary>
    public static class UIDispatcher
    {
        private static Dispatcher dispatcher;

        static UIDispatcher()
        {
#if SILVERLIGHT
            dispatcher = Deployment.Current.Dispatcher;
#else
            dispatcher = Dispatcher.CurrentDispatcher;
#endif
        }

#if !SILVERLIGHT
        /// <summary>
        /// Invoke from main UI thread.
        /// </summary>
        public static void Initialize()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
        }

#endif
        /// <summary>
        /// Obtain the current dispatcher for cross-thread marshaling
        /// </summary>
        public static Dispatcher Current
        {
            get
            {
                return dispatcher;
            }
        }

        /// <summary>
        /// Execute an action on the UI thread.
        /// </summary>
        /// <param name="action"></param>
        public static void Execute(Action action)
        {
            if (dispatcher.CheckAccess()) action();
            else dispatcher.BeginInvoke(action);
        }
    }
}
