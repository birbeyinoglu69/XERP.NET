using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.AddressDomain.AddressDataService;
using XERP.Domain.AddressDomain.Services;

namespace XERP.Domain.AddressDomain
{
    public class AddressSingletonRepository
    {
        private AddressSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new AddressEntities(_rootUri);
        }
        
        private static AddressSingletonRepository _instance;
        public static AddressSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AddressSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private AddressEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<Address> GetAddresses(string companyID)
        {
            _repositoryContext = new AddressEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Addresses
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<Address> GetAddresses(Address addressQuerryObject, string companyID)
        {
            _repositoryContext = new AddressEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.Addresses
                              where q.CompanyID == companyID
                             select q;
            if  (!string.IsNullOrEmpty(addressQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(addressQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(addressQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(addressQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Address1))
                queryResult = queryResult.Where(q => q.Address1.StartsWith(addressQuerryObject.Address1.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Address2))
                queryResult = queryResult.Where(q => q.Address2.StartsWith(addressQuerryObject.Address2.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Address3))
                queryResult = queryResult.Where(q => q.Address3.StartsWith(addressQuerryObject.Address3.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.City))
                queryResult = queryResult.Where(q => q.City.StartsWith(addressQuerryObject.City.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.State))
                queryResult = queryResult.Where(q => q.State.StartsWith(addressQuerryObject.State.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Zip))
                queryResult = queryResult.Where(q => q.Zip.StartsWith(addressQuerryObject.Zip.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Country))
                queryResult = queryResult.Where(q => q.Country.StartsWith(addressQuerryObject.Country.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.PhoneNum))
                queryResult = queryResult.Where(q => q.PhoneNum.StartsWith(addressQuerryObject.PhoneNum.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.PhoneNum2))
                queryResult = queryResult.Where(q => q.PhoneNum2.StartsWith(addressQuerryObject.PhoneNum2.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.FaxNum))
                queryResult = queryResult.Where(q => q.FaxNum.StartsWith(addressQuerryObject.FaxNum.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Email))
                queryResult = queryResult.Where(q => q.Email.StartsWith(addressQuerryObject.Email.ToString()));

            if (!string.IsNullOrEmpty(addressQuerryObject.Email2))
                queryResult = queryResult.Where(q => q.Email2.StartsWith(addressQuerryObject.Email2.ToString()));

            return queryResult;
        }

        public IEnumerable<Address> GetAddressByID(string addressID, string companyID)
        {
            _repositoryContext = new AddressEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Addresses
                          where q.AddressID == addressID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<Address> Refresh(string autoIDs)
        {
            _repositoryContext = new AddressEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<Address>("RefreshAddress").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(Address address)
        {
            if (_repositoryContext.GetEntityDescriptor(address) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(address);
            }
        }

        public void AddToRepository(Address address)
        {
            address.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToAddresses(address);
        }

        public void DeleteFromRepository(Address address)
        {
            if (_repositoryContext.GetEntityDescriptor(address) != null)
            {//if it exists in the db delete it from the db
                AddressEntities context = new AddressEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                Address deletedAddress = (from q in context.Addresses
                                          where q.AddressID == address.AddressID
                                          select q).SingleOrDefault();
                if (deletedAddress != null)
                {
                    context.DeleteObject(deletedAddress);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetAddressEntityState(address) != EntityStates.Detached)
                    _repositoryContext.Detach(address);
            }
        }

        public EntityStates GetAddressEntityState(Address address)
        {
            if (_repositoryContext.GetEntityDescriptor(address) != null)
                return _repositoryContext.GetEntityDescriptor(address).State;
            else
                return EntityStates.Detached;
        }   
    }
}
