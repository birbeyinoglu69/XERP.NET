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

        private string _systemUserID;
        public string SystemUserID
        {
            get { return _systemUserID; }
            set { _systemUserID = value; }
        }

        private string _companyID;
        public string CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }
        
    }
}
