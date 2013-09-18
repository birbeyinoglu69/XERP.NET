
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.DBStoredImageMaintenance.ViewModels;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Client.WPF.DBStoredImageMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IDBStoredImageServiceAgent serviceAgent = new DBStoredImageServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IDBStoredImageServiceAgent serviceAgent = new DBStoredImageServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }
    }
}