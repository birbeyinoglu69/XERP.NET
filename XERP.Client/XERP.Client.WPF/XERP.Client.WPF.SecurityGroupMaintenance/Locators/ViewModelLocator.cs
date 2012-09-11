
// Toolkit namespace

//XERP namespaces
using XERP.Client.WPF.SecurityGroupMaintenance.ViewModels;
using XERP.Domain.SecurityGroupDomain.Services;

namespace XERP.Client.WPF.SecurityGroupMaintenance
{
    public class ViewModelLocator
    {
        public MainMaintenanceViewModel MainMaintenanceViewModel
        {
            get
            {
                ISecurityGroupServiceAgent serviceAgent = new SecurityGroupServiceAgent();
                return new MainMaintenanceViewModel(serviceAgent);
            }
        }

        public MainSearchViewModel MainSearchViewModel
        {
            get
            {
                ISecurityGroupServiceAgent serviceAgent = new SecurityGroupServiceAgent();
                return new MainSearchViewModel(serviceAgent);
            }
        }

        public TypeSearchViewModel TypeSearchViewModel
        {
            get
            {
                ISecurityGroupServiceAgent serviceAgent = new SecurityGroupServiceAgent();
                return new TypeSearchViewModel(serviceAgent);
            }
        }

        public TypeMaintenanceViewModel TypeMaintenanceViewModel
        {
            get
            {
                ISecurityGroupServiceAgent serviceAgent = new SecurityGroupServiceAgent();
                return new TypeMaintenanceViewModel(serviceAgent);
            }
        }

        public CodeSearchViewModel CodeSearchViewModel
        {
            get
            {
                ISecurityGroupServiceAgent serviceAgent = new SecurityGroupServiceAgent();
                return new CodeSearchViewModel(serviceAgent);
            }
        }

        public CodeMaintenanceViewModel CodeMaintenanceViewModel
        {
            get
            {
                ISecurityGroupServiceAgent serviceAgent = new SecurityGroupServiceAgent();
                return new CodeMaintenanceViewModel(serviceAgent);
            }
        }
    }
}