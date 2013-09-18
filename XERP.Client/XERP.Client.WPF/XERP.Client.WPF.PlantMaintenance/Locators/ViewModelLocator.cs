
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.PlantMaintenance.ViewModels;
using XERP.Domain.PlantDomain.Services;

namespace XERP.Client.WPF.PlantMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IPlantServiceAgent serviceAgent = new PlantServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IPlantServiceAgent serviceAgent = new PlantServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                IPlantServiceAgent serviceAgent = new PlantServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                IPlantServiceAgent serviceAgent = new PlantServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                IPlantServiceAgent serviceAgent = new PlantServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                IPlantServiceAgent serviceAgent = new PlantServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}