using System;
using XERP.Client;

namespace XERP.Domain.AddressDomain.Services
{
    class ServiceUtility
    {
        private const string _dataServicePortNumber = "1215";
        public string DataServicePortNumber
        {
            get { return _dataServicePortNumber; }
        }
        private const string _dataServiceName = "AddressDataService.svc";
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