using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Windows.Threading;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    public abstract class ModelBase<TModel> : INotifyPropertyChanged, INotifyDataErrorInfo
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

        #region Validation

        readonly Dictionary<string, List<string>> errors =
            new Dictionary<string,List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            CheckErrors(propertyName);
            foreach (var error in errors[propertyName])
            {
                yield return error;
            }
        }

        public bool HasErrors
        {
            get
            {
                return (errors.Where(c => c.Value.Count > 0).Count() > 0); 
            }
        }

        /// <summary>
        /// Allows you to specify a lambda for property validation
        /// </summary>
        /// <typeparam name="TResult">Property type</typeparam>
        /// <param name="property">Property for validation</param>
        /// <param name="value">Value being validated</param>
        protected virtual void ValidateProperty<TResult>
            (Expression<Func<TModel, TResult>> property, object value)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Validate property
            InternalValidateProperty(propertyName, value);
        }

        private void InternalValidateProperty(string propertyName, object value)
        {
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = propertyName }, results);
            if (isValid)
            {
                RemoveErrors(propertyName);
            }
            else
            {
                AddErrors(propertyName, results);
            }
            NotifyErrorsChanged(propertyName);
            BindingHelper.InternalNotifyPropertyChanged("HasErrors", this, propertyChanged);
        }

        private void AddErrors(string propertyName, List<ValidationResult> results)
        {
            RemoveErrors(propertyName);
            errors[propertyName].AddRange(results.Select(vr => vr.ErrorMessage));
        }

        private void RemoveErrors(string propertyName)
        {
            CheckErrors(propertyName);
            errors[propertyName].Clear();
        }

        private void CheckErrors(string propertyName)
        {
            if (!errors.ContainsKey(propertyName))
            {
                errors[propertyName] = new List<string>();
            }
        }

        private void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
