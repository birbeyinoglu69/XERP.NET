using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.UdListDomain.UdListDataService;
namespace XERP.Domain.UdListDomain.Services
{
    public class UdListServiceAgent : XERP.Domain.UdListDomain.Services.IUdListServiceAgent 
    {
        #region Initialize Service
        public UdListServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new UdListEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private UdListEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool UdListExists(string udListID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.UdLists
                           where q.UdListID == udListID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }
        
        public bool UdListItemExists(string udListID, string udListItemID, string companyID, int autoID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.UdListItems
                               where q.UdListID == udListID &&
                               q.UdListItemID == udListItemID &&
                               q.CompanyID == companyID &&
                               q.AutoID != autoID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }

        public bool RepositoryIsDirty()
        {
            return UdListSingletonRepository.Instance.RepositoryIsDirty();
        }   
        #endregion Read Only Methods  No Repository Required

        #region UdList Repository CRUD
        public IEnumerable<UdList> RefreshUdList(string autoIDs)
        {
            return UdListSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<UdList> GetUdLists(string companyID)
        {
            return UdListSingletonRepository.Instance.GetUdLists(companyID);
        }

        public IEnumerable<UdList> GetUdLists(UdList udListQuerryObject, string companyID)
        {
            return UdListSingletonRepository.Instance.GetUdLists(udListQuerryObject, companyID);
        }

        public IEnumerable<UdList> GetUdListByID(string udListID, string companyID)
        {
            return UdListSingletonRepository.Instance.GetUdListByID(udListID, companyID);
        }

        public void CommitUdListRepository()
        {
            //null any and all client variables...
            UdListSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateUdListRepository(UdList udList)
        {//make sure client only fields are never updated to the db...
            UdListSingletonRepository.Instance.UpdateRepository(udList);
        }

        public void UpdateUdListRepository(UdListItem udListItem)
        {//make sure client only fields are never updated to the db...
            UdListSingletonRepository.Instance.UpdateRepository(udListItem);
        }

        public void AddToUdListRepository(UdList udList)
        {
            UdListSingletonRepository.Instance.AddToRepository(udList);
        }

        public void AddToUdListRepository(UdListItem udListItem)
        {
            UdListSingletonRepository.Instance.AddToRepository(udListItem);
        }

        public void DeleteFromUdListRepository(UdList udList)
        {
            UdListSingletonRepository.Instance.DeleteFromRepository(udList);
        }

        public void DeleteFromUdListRepository(UdListItem udListItem)
        {
            UdListSingletonRepository.Instance.DeleteFromRepository(udListItem);
        }

        public EntityStates GetUdListEntityState(UdList udList)
        {
            return UdListSingletonRepository.Instance.GetUdListEntityState(udList);
        }

        public EntityStates GetUdListItemEntityState(UdListItem udListItem)
        {
            return UdListSingletonRepository.Instance.GetUdListItemEntityState(udListItem);
        }

        public UdList UdListCache
        {
            get { return UdListSingletonRepository.Instance.UdListCahe; }
            set { UdListSingletonRepository.Instance.UdListCahe = value; }
        }
        #endregion UdList Repository CRUD
    }
}
