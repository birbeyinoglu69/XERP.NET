
namespace XERP.Client.WPF.UdListMaintenance
{//use this class to share properties amonst views, viewmodels etc...
    public class GlobalProperties
    {//UdListMaintenace is the exutable program that this Maintenance UI is tied to...
        //All UI's used within this project will inherit this executableprogram's security privelages...
        //this program name coencides with db table ExecutablPrograms...
        //UdLists are then applied to the ExecutablePrograms allotting for custom form athentication...
        private const string _executableProgramName = "UdListMaintenance";
        public string ExecutableProgramName
        {
            get { return _executableProgramName; }
        }

        
        
    }

}
