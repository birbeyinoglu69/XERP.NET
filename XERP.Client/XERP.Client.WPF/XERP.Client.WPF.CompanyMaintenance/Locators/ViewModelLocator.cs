using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

//XERP namespaces
using XERP.Client.WPF.CompanyMaintenance.ViewModels;
using XERP.Domain.CompanyDomain.Services;

namespace XERP.Client.WPF.CompanyMaintenance
{
    public class ViewModelLocator
    {
        //public MainPageViewModel MainPageViewModel
        //{
        //    get { return new MainPageViewModel(); }
        //}

        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}