using System;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMvvmToolkit.TestExtensions
{
    /// <summary>
    /// Extensions for SilverlightTest class.
    /// </summary>
    public static class SilverlightTestExtensions
    {
        /// <summary>
        /// Requires a bool returning delegate to be passed in. Instructs the test task
        /// queue to wait until the conditional call returns True to continue executing
        /// other test tasks and/or ending the test method.
        /// </summary>
        /// <param name="test">Silverlight test class</param>
        /// <param name="condition">Conditional method or delegate. Test will halt until this condition returns True</param>
        /// <param name="seconds">Number of seconds before timeout expires. Pass Timeout.Infinite for no timeout.</param>
        public static void EnqueueConditional(this SilverlightTest test,
            Func<bool> condition, int seconds)
        {
            DateTime startTime = DateTime.Now;
            TimeSpan duration = TimeSpan.FromSeconds(seconds);
            test.EnqueueConditional(() =>
            {
                if (seconds != Timeout.Infinite)
                {
                    string message = string.Format("Condition not satisfied before timeout of {0} seconds", seconds);
                    Assert.IsTrue(DateTime.Now < startTime.Add(duration), message);
                }
                return condition();
            });
        }

        public static string GetPropertyName<TViewModel, TResult>
            (this SilverlightTest test, Expression<Func<TViewModel, TResult>> property)
        {
            string propertyName = ((MemberExpression)property.Body).Member.Name;
            return propertyName;
        }
    }
}
