
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.UdListMaintenance.ViewModels;
using XERP.Domain.UdListDomain.Services;

namespace XERP.Client.WPF.UdListMaintenance
{
    public class ViewModelLocator
    {//this will allow for a singleton viewmodel per Maintenance form...
        //allowing us to share viewmodels amongst multiple views...
        //any child views requiring the same view model will use this one...
        
        private ViewModelLocator()
        {

        }
        private static ViewModelLocator _instance;
        public static ViewModelLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ViewModelLocator();

                }
                return _instance;
            }
        }

        public MainMaintenanceViewModel SharedMainMaintenanceViewModel;
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IUdListServiceAgent serviceAgent = new UdListServiceAgent();
                SharedMainMaintenanceViewModel = new MainMaintenanceViewModel(serviceAgent);
                return SharedMainMaintenanceViewModel;
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IUdListServiceAgent serviceAgent = new UdListServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }
    }
}