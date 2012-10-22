using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XERP.Client.WPF.UdListMaintenance
{//singleton will not reference in xaml so we shell it with a standard class that we can reference to...
    public class ViewModelLocatorProvider
    {
        public ViewModelLocator ViewModelLocator { get { return ViewModelLocator.Instance; } }
    }
}
