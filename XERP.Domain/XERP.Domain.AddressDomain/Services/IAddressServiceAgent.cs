using System;
namespace XERP.Domain.AddressDomain.Services
{
    public interface IAddressServiceAgent
    {
        bool AddressExists(string addressID, string companyID);
        bool AddressRepositoryIsDirty();
        void AddToAddressRepository(XERP.Domain.AddressDomain.AddressDataService.Address address);
        void CommitAddressRepository();
        void DeleteFromAddressRepository(XERP.Domain.AddressDomain.AddressDataService.Address address);
        System.Collections.Generic.IEnumerable<XERP.Domain.AddressDomain.AddressDataService.Address> GetAddressByID(string addressID, string companyID);
        System.Data.Services.Client.EntityStates GetAddressEntityState(XERP.Domain.AddressDomain.AddressDataService.Address address);
        System.Collections.Generic.IEnumerable<XERP.Domain.AddressDomain.AddressDataService.Address> GetAddresses(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.AddressDomain.AddressDataService.Address> GetAddresses(XERP.Domain.AddressDomain.AddressDataService.Address addressQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.AddressDomain.AddressDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.AddressDomain.AddressDataService.Address> RefreshAddress(string autoIDs);
        void UpdateAddressRepository(XERP.Domain.AddressDomain.AddressDataService.Address address);
    }
}
