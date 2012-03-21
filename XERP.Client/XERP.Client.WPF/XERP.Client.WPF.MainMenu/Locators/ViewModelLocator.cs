using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

//XERP namespaces
using XERP.Client.WPF.MainMenu.ViewModels;
using XERP.MenuSecurityDomain.Services;

namespace XERP.Client.WPF.MainMenu
{         
    public class ViewModelLocator
    {
        // Create MainPageViewModel on demand
        public MainPageViewModel MainPageViewModel
        {
            get { return new MainPageViewModel(); }
        }

        public MainMenuViewModel MainMenuViewModel
        {
            get
            {
                IMenuSecurityServiceAgent serviceAgent = new MenuSecurityServiceAgent();
                return new MainMenuViewModel(serviceAgent);
            }
        }


    }
}