using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.UdListDomain.UdListDataService;
using XERP.Domain.UdListDomain.Services;

namespace XERP.Domain.UdListDomain
{
    public class UdListSingletonRepository
    {
        private UdListSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new UdListEntities(_rootUri);
        }
        
        private static UdListSingletonRepository _instance;
        public static UdListSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UdListSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private UdListEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return  _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<UdList> GetUdLists(string companyID)
        {
            _repositoryContext = new UdListEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.UdLists.Expand("UdListItems")
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<UdList> GetUdLists(UdList udListQuerryObject, string companyID)
        {
            _repositoryContext = new UdListEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.UdLists.Expand("UdListItems")
                              where q.CompanyID == companyID
                             select q; 
            if  (!string.IsNullOrEmpty(udListQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(udListQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(udListQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(udListQuerryObject.Description.ToString()));

            return queryResult;
        }

        public IEnumerable<UdList> GetUdListByID(string udListID, string companyID)
        {
            _repositoryContext = new UdListEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.UdLists.Expand("UdListItems")
                          where q.UdListID == udListID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<UdList> Refresh(string autoIDs)
        {
            _repositoryContext = new UdListEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<UdList>("RefreshUdList").Expand("UdListItems").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(UdList item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void UpdateRepository(UdListItem item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(UdList udList)
        {
            udList.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToUdLists(udList);
        }

        public void AddToRepository(UdListItem udListItem)
        {
            udListItem.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToUdListItems(udListItem);
        }

        public void DeleteFromRepository(UdList udList)
        {
            if (_repositoryContext.GetEntityDescriptor(udList) != null)
            {
                //if it exists in the db delete it from the db
                UdListEntities context = new UdListEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                UdList deletedUdList = (from q in context.UdLists
                                          where q.UdListID == udList.UdListID &&
                                                q.CompanyID == udList.CompanyID
                                          select q).FirstOrDefault();
                if (deletedUdList != null)
                {
                    context.DeleteObject(deletedUdList);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetUdListEntityState(udList) != EntityStates.Detached)
                    _repositoryContext.Detach(udList);
            }
        }

        public void DeleteFromRepository(UdListItem udListItem)
        {
            if (_repositoryContext.GetEntityDescriptor(udListItem) != null)
            {
                //if it exists in the db delete it from the db
                UdListEntities context = new UdListEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                UdListItem deletequery = (from q in context.UdListItems
                                        where q.UdListItemID == udListItem.UdListItemID &&
                                                q.CompanyID == udListItem.CompanyID
                                        select q).FirstOrDefault();
                if (deletequery != null)
                {
                    context.DeleteObject(deletequery);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetUdListItemEntityState(udListItem) != EntityStates.Detached)
                    _repositoryContext.Detach(udListItem);
            }
        }

        public EntityStates GetUdListEntityState(UdList udList)
        {
            if (_repositoryContext.GetEntityDescriptor(udList) != null)
                return _repositoryContext.GetEntityDescriptor(udList).State;
            else
                return EntityStates.Detached;
        }

        public EntityStates GetUdListItemEntityState(UdListItem udListItem)
        {
            if (_repositoryContext.GetEntityDescriptor(udListItem) != null)
                return _repositoryContext.GetEntityDescriptor(udListItem).State;
            else
                return EntityStates.Detached;
        }

        //used to cache the context amongst multiple views...
        private UdList _udListCahe;
        public UdList UdListCahe
        {
            get { return _udListCahe; }
            set { _udListCahe = value; }
        }
    }
}
