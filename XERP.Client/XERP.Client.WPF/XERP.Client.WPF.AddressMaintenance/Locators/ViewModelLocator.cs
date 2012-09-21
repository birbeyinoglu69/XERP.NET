
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.AddressMaintenance.ViewModels;
using XERP.Domain.AddressDomain.Services;

namespace XERP.Client.WPF.AddressMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                IAddressServiceAgent serviceAgent = new AddressServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                IAddressServiceAgent serviceAgent = new AddressServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }
    }
}