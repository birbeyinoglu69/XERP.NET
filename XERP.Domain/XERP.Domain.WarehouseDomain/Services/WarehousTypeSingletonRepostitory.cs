using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain
{
    public class WarehouseTypeSingletonRepository
    {
        private WarehouseTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }

        private static WarehouseTypeSingletonRepository _instance;
        public static WarehouseTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseType> GetWarehouseTypes(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseType> GetWarehouseTypes(WarehouseType itemTypeQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseTypes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.WarehouseTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.WarehouseTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<WarehouseType> GetWarehouseTypeByID(string itemTypeID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseTypes
                               where q.WarehouseTypeID == itemTypeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseType> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseType>("RefreshWarehouseType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(WarehouseType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseTypes(itemType);
        }

        public void DeleteFromRepository(WarehouseType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {//if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseType deletedWarehouseType = (from q in context.WarehouseTypes
                                          where q.WarehouseTypeID == itemType.WarehouseTypeID
                                          select q).FirstOrDefault();
                if (deletedWarehouseType != null)
                {
                    context.DeleteObject(deletedWarehouseType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetWarehouseTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetWarehouseTypeEntityState(WarehouseType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
