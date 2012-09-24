
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.MenuItemMaintenance.ViewModels;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Client.WPF.MenuItemMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IMenuItemServiceAgent serviceAgent = new MenuItemServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IMenuItemServiceAgent serviceAgent = new MenuItemServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                IMenuItemServiceAgent serviceAgent = new MenuItemServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                IMenuItemServiceAgent serviceAgent = new MenuItemServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                IMenuItemServiceAgent serviceAgent = new MenuItemServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                IMenuItemServiceAgent serviceAgent = new MenuItemServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}