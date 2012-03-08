using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

//XERP namespaces
using XERP.Client.WPF.CompanyMaintenance.ViewModels;
//using XERP.CompanyDomain.Services;
using XERP.CompanyDomain.Services;

namespace XERP.Client.WPF.CompanyMaintenance
{
    public class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel
        {
            get { return new MainPageViewModel(); }
        }

        public CompanyMaintenanceViewModel CompanyMaintenanceViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new CompanyMaintenanceViewModel(serviceAgent);
            }
        }

        public CompanySearchViewModel CompanySearchViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new CompanySearchViewModel(serviceAgent);
            }
        }
    }
}