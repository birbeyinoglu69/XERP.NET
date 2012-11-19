using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.AddressDomain.AddressDataService;
namespace XERP.Domain.AddressDomain.Services
{
    public class AddressServiceAgent : XERP.Domain.AddressDomain.Services.IAddressServiceAgent
    {
        #region Initialize Service
        public AddressServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new AddressEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private AddressEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool AddressRepositoryIsDirty()
        {
            return AddressSingletonRepository.Instance.RepositoryIsDirty();
        }
 
        public bool AddressExists(string addressID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Addresses
                           where q.AddressID == addressID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {//WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }
        #endregion Read Only Methods  No Repository Required

        #region Address Repository CRUD
        public IEnumerable<Address> RefreshAddress(string autoIDs)
        {
            return AddressSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<Address> GetAddresses(string companyID)
        {
            return AddressSingletonRepository.Instance.GetAddresses(companyID);
        }

        public IEnumerable<Address> GetAddresses(Address addressQuerryObject, string companyID)
        {
            return AddressSingletonRepository.Instance.GetAddresses(addressQuerryObject, companyID);
        }

        public IEnumerable<Address> GetAddressByID(string addressID, string companyID)
        {
            return AddressSingletonRepository.Instance.GetAddressByID(addressID, companyID);
        }

        public void CommitAddressRepository()
        {
            AddressSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateAddressRepository(Address address)
        {
            AddressSingletonRepository.Instance.UpdateRepository(address);
        }

        public void AddToAddressRepository(Address address)
        {
            AddressSingletonRepository.Instance.AddToRepository(address);
        }

        public void DeleteFromAddressRepository(Address address)
        {
            AddressSingletonRepository.Instance.DeleteFromRepository(address);
        }

        public EntityStates GetAddressEntityState(Address address)
        {
            return AddressSingletonRepository.Instance.GetAddressEntityState(address);
        }
        #endregion Address Repository CRUD
    }
}
