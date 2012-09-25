
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.ExecutableProgramMaintenance.ViewModels;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Client.WPF.ExecutableProgramMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IExecutableProgramServiceAgent serviceAgent = new ExecutableProgramServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IExecutableProgramServiceAgent serviceAgent = new ExecutableProgramServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                IExecutableProgramServiceAgent serviceAgent = new ExecutableProgramServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                IExecutableProgramServiceAgent serviceAgent = new ExecutableProgramServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                IExecutableProgramServiceAgent serviceAgent = new ExecutableProgramServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                IExecutableProgramServiceAgent serviceAgent = new ExecutableProgramServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}