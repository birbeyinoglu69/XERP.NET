
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.WarehouseMaintenance.ViewModels;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Client.WPF.WarehouseMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IWarehouseServiceAgent serviceAgent = new WarehouseServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IWarehouseServiceAgent serviceAgent = new WarehouseServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                IWarehouseServiceAgent serviceAgent = new WarehouseServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                IWarehouseServiceAgent serviceAgent = new WarehouseServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                IWarehouseServiceAgent serviceAgent = new WarehouseServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                IWarehouseServiceAgent serviceAgent = new WarehouseServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}