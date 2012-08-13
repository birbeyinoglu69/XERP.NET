using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.Client;

namespace XERP.Domain.LogInDomain.Services
{
    class ServiceUtility
    {
        private const string _dataServicePortNumber = "1200";
        public  string DataServicePortNumber
        {
            get { return _dataServicePortNumber; }
        }
        private const string _dataServiceName = "LogInDataService.svc";
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
