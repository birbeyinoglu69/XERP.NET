using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.Client;

namespace XERP.MenuSecurityDomain.Services
{
    class ServiceUtility
    {
        private const string _dataServicePortNumber = "1202";
        public  string DataServicePortNumber
        {
            get { return _dataServicePortNumber; }
        }
        private const string _dataServiceName = "MenuSecurityService.svc";
        public string DataServiceName
        {
            get { return _dataServiceName; }
        }
        public Uri BaseUri
        {
            get
            {
                return new Uri(ClientSessionSingleton.Instance.ConfigURI + ":" + _dataServicePortNumber + "/" + _dataServiceName);
            }
        }

    }
}
