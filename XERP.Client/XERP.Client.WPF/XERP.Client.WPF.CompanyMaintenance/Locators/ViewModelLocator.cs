
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.CompanyMaintenance.ViewModels;
using XERP.Domain.CompanyDomain.Services;

namespace XERP.Client.WPF.CompanyMaintenance
{
    public class ViewModelLocator
    {
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

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                ICompanyServiceAgent serviceAgent = new CompanyServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}