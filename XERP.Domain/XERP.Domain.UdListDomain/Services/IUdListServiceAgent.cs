using System;
namespace XERP.Domain.UdListDomain.Services
{
    public interface IUdListServiceAgent
    {
        void AddToUdListRepository(XERP.Domain.UdListDomain.UdListDataService.UdList udList);
        void AddToUdListRepository(XERP.Domain.UdListDomain.UdListDataService.UdListItem udListItem);
        void CommitUdListRepository();
        void DeleteFromUdListRepository(XERP.Domain.UdListDomain.UdListDataService.UdList udList);
        void DeleteFromUdListRepository(XERP.Domain.UdListDomain.UdListDataService.UdListItem udListItem);
        System.Collections.Generic.IEnumerable<XERP.Domain.UdListDomain.UdListDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.UdListDomain.UdListDataService.UdList> GetUdListByID(string udListID, string companyID);
        System.Data.Services.Client.EntityStates GetUdListEntityState(XERP.Domain.UdListDomain.UdListDataService.UdList udList);
        System.Data.Services.Client.EntityStates GetUdListItemEntityState(XERP.Domain.UdListDomain.UdListDataService.UdListItem udListItem);
        System.Collections.Generic.IEnumerable<XERP.Domain.UdListDomain.UdListDataService.UdList> GetUdLists(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.UdListDomain.UdListDataService.UdList> GetUdLists(XERP.Domain.UdListDomain.UdListDataService.UdList udListQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.UdListDomain.UdListDataService.UdList> RefreshUdList(string autoIDs);
        bool RepositoryIsDirty();
        XERP.Domain.UdListDomain.UdListDataService.UdList UdListCache { get; set; }
        bool UdListExists(string udListID, string companyID);
        bool UdListItemExists(string udListID, string udListItemID, string companyID, int autoID);
        void UpdateUdListRepository(XERP.Domain.UdListDomain.UdListDataService.UdList udList);
        void UpdateUdListRepository(XERP.Domain.UdListDomain.UdListDataService.UdListItem udListItem);
    }
}
