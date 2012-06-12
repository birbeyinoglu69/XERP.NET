using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XERP.Client.WPF.CompanyMaintenance
{//use this class to share properties amonst views, viewmodels etc...
    public class GlobalProperties
    {//CompanyMaintenace is the exutable program that this Maintenance UI is tied to...
        //All UI's used within this project will inherit this executableprogram's security privelages...
        //this program name coencides with db table ExecutablPrograms...
        //SecurityGroups are then applied to the ExecutablePrograms allotting for custom form athentication...
        private const string _executableProgramName = "CompanyMaintenance";
        public string ExecutableProgramName
        {
            get { return _executableProgramName; }
        } 

    }
}
