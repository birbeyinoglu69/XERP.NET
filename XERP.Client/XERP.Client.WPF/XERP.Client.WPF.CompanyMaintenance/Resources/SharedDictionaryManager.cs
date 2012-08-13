using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace XERP.Client.WPF.CompanyMaintenance.Resources
{
    internal static class SharedDictionaryManager
    {
        private static ResourceDictionary _menuImagesSharedDictionary;
        internal static ResourceDictionary MenuImagesSharedDictionary
        {
            get
            {
                if (_menuImagesSharedDictionary == null)
                {
                    System.Uri resourceLocater =
                        new System.Uri("/XERP.Client.WPF;component/Resources/MenuImages.xaml",
                                        System.UriKind.Relative);

                    _menuImagesSharedDictionary =
                        (ResourceDictionary)Application.LoadComponent(resourceLocater);

                }
                return _menuImagesSharedDictionary;
            }
        }
        private static ResourceDictionary _baseControlsSharedDictionary;
        internal static ResourceDictionary BaseControlsSharedDictionary
        {
            get
            {
                if (_baseControlsSharedDictionary == null)
                {
                    System.Uri resourceLocater =
                        new System.Uri("/XERP.Client.WPF;component/Resources/BaseControls.xaml",
                                        System.UriKind.Relative);

                    _baseControlsSharedDictionary =
                        (ResourceDictionary)Application.LoadComponent(resourceLocater);

                }
                return _baseControlsSharedDictionary;
            }
        }
    }
}
