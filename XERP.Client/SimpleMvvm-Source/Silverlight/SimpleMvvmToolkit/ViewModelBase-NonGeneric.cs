using System;
using System.Windows;
using System.ComponentModel;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Base class for view models
    /// </summary>
    public abstract class ViewModelBase
    {
        /// <summary>
        /// Allows you to provide data at design-time (Blendability)
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
#if SILVERLIGHT
                return DesignerProperties.IsInDesignTool;
#else
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    bool isInDesignMode = (bool)DependencyPropertyDescriptor
                        .FromProperty(prop, typeof(FrameworkElement))
                        .Metadata.DefaultValue;
                    return isInDesignMode;
#endif
            }
        }
    }
}
