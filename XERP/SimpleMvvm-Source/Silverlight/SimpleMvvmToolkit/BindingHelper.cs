using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Threading;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Consolidated data binding helper methods
    /// </summary>
    public static class BindingHelper
    {
        internal static void NotifyPropertyChanged<TModel, TResult>
            (Expression<Func<TModel, TResult>> property,
            object sender, PropertyChangedEventHandler propertyChanged,
            Dispatcher dispatcher)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Fire notify property changed event
            InternalNotifyPropertyChanged(propertyName, sender, propertyChanged, dispatcher);
        }

        /// <summary>
        /// Defined as an extension method for use by subclasses.
        /// Usage: this.NotifyPropertyChanged(m => m.PropertyName, propertyChanged);
        /// </summary>
        /// <typeparam name="TModel">ViewModel or model property type</typeparam>
        /// <typeparam name="TResult">Property result type</typeparam>
        /// <param name="model">ViewModel or model</param>
        /// <param name="property">ViewModel or model property</param>
        /// <param name="propertyChanged">PropertyChanged event</param>
        public static void NotifyPropertyChanged<TModel, TResult>
            (this TModel model, Expression<Func<TModel, TResult>> property,
            PropertyChangedEventHandler propertyChanged)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Fire notify property changed event
            InternalNotifyPropertyChanged(propertyName, model, propertyChanged);
        }

        internal static void InternalNotifyPropertyChanged(string propertyName,
            object sender, PropertyChangedEventHandler propertyChanged)
        {
            InternalNotifyPropertyChanged(propertyName, sender, propertyChanged,
                UIDispatcher.Current);
        }

        internal static void InternalNotifyPropertyChanged(string propertyName,
            object sender, PropertyChangedEventHandler propertyChanged, Dispatcher dispatcher)
        {
            if (propertyChanged != null)
            {
                // Always fire the event on the UI thread
                if (dispatcher.CheckAccess())
                {
                    propertyChanged(sender, new PropertyChangedEventArgs(propertyName));

                }
                else
                {
                    Action action = () => propertyChanged
                        (sender, new PropertyChangedEventArgs(propertyName));
                    dispatcher.BeginInvoke(action);
                }
            }
        }
    }
}
