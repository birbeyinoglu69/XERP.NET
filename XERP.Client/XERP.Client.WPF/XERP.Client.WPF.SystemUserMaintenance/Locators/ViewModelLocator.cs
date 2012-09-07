
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.SystemUserMaintenance.ViewModels;
using XERP.Domain.SystemUserDomain.Services;

namespace XERP.Client.WPF.SystemUserMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                ISystemUserServiceAgent serviceAgent = new SystemUserServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                ISystemUserServiceAgent serviceAgent = new SystemUserServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                ISystemUserServiceAgent serviceAgent = new SystemUserServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                ISystemUserServiceAgent serviceAgent = new SystemUserServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                ISystemUserServiceAgent serviceAgent = new SystemUserServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                ISystemUserServiceAgent serviceAgent = new SystemUserServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}