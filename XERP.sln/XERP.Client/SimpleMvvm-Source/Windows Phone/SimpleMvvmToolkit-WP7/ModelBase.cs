using System;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Windows.Threading;

namespace SimpleMvvmToolkit
{
    // This class may not be necessary if code-generated model classes
    // already implement INotifyPropertyChanged. It is included here
    // in case you are creating model classes from scratch.

    /// <summary>
    /// Provides support to entities for two-way data binding by
    /// implementing INotifyPropertyChanged with a lambda expression.
    /// </summary>
    /// <typeparam name="TModel">Class inheriting from ModelBase</typeparam>
    [DataContract]
    public abstract class ModelBase<TModel> : INotifyPropertyChanged
    {
        #region Data Binding

        // Dispatcher for cross-thread operations
        Dispatcher dispatcher = UIDispatcher.Current;

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
            (Expression<Func<TModel, TResult>> property)
        {
            // Fire PropertyChanged event
            BindingHelper.NotifyPropertyChanged(property, this, propertyChanged, dispatcher);
        }

        #endregion
    }
}
