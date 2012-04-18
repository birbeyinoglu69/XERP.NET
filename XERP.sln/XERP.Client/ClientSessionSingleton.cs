using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XERP.Client
{
    public class ClientSessionSingleton
    {
        private static ClientSessionSingleton _instance;
        private ClientSessionSingleton() 
        {
            
        }

        public static ClientSessionSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientSessionSingleton();
                }
                return _instance;
            }
        }

        private XERPClientConfig _config = new XERPClientConfig();
        public string ConfigURI
        {
            get 
            {
                return _config.ConfigURI; ; 
            }
        }
        //cache systemuserID
        private string _systemUserID;
        public string SystemUserID
        {
            get { return _systemUserID; }
            set { _systemUserID = value; }
        }
        //cache companyid
        private string _companyID;
        public string CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }
        //cache securitygroups user belongs too...
        //too prevent exess trips to the db...
        private List<string> _securityGroupIDList = new List<string>();
        public List<string> SecurityGroupIDList
        {
            get { return _securityGroupIDList; }
            set { _securityGroupIDList = value; }
        }
        //cache ExecutablePrograms User has Access to...
        //to prevent exess trips to the db...
        private List<string> _executableProgramIDList = new List<string>();
        public List<string> ExecutableProgramIDList
        {
            get { return _executableProgramIDList; }
            set { _executableProgramIDList = value; }
        }

        //XERP AuthenticationProtectionFlag...
        //When user is authenticated by user login then we will trip this flag
        //This will allow us to maintain that the UI's were opened from the Security
        //Driven Main Menu...
        private bool _sessionIsAuthentic = false;
        public bool SessionIsAuthentic
        {
            get { return _sessionIsAuthentic; }
            set { _sessionIsAuthentic = value; }
        }

        //XERP is not authenticated message
        private string _sessionNotAuthenticatedMessage = "XERP Session Is Not Authenticated.";
        public string SessionNotAuthenticatedMessage
        {
            get { return _sessionNotAuthenticatedMessage; }
            //set { _sessionNotAuthenticatedMessage = value; }
        }
    }
}
