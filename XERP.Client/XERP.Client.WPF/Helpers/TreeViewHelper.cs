using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Net; 
using System.Windows.Documents; 
using System.Windows.Ink; 
using System.Windows.Input; 
using System.Windows.Media; 
using System.Windows.Media.Animation; 
using System.Windows.Shapes; 
using System.ComponentModel; 
namespace XERP.Client.WPF.Helpers
{
    public class ExtendedTreeView : TreeView 
    { 
        public ExtendedTreeView() : base() 
        { 
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(___ICH); 
        } 
        void ___ICH(object sender, RoutedPropertyChangedEventArgs<object> e) 
        {
            SetValue(SelectedItem_Property, SelectedItem); 
        } 
        public object SelectedItem_ 
        { 
            get { return (object)GetValue(SelectedItem_Property); } 
            set { SetValue(SelectedItem_Property, value); } 
        } 
        public static readonly DependencyProperty SelectedItem_Property = 
            DependencyProperty.Register("SelectedItem_", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null)); 
    } 
}
