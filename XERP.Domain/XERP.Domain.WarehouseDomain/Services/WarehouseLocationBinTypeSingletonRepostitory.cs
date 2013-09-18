using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain
{
    public class WarehouseLocationBinTypeSingletonRepository
    {
        private WarehouseLocationBinTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }

        private static WarehouseLocationBinTypeSingletonRepository _instance;
        public static WarehouseLocationBinTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseLocationBinTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypes(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationBinTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypes(WarehouseLocationBinType itemTypeQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseLocationBinTypes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.WarehouseLocationBinTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.WarehouseLocationBinTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<WarehouseLocationBinType> GetWarehouseLocationBinTypeByID(string itemTypeID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationBinTypes
                               where q.WarehouseLocationBinTypeID == itemTypeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBinType> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseLocationBinType>("RefreshWarehouseLocationBinType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseLocationBinType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(WarehouseLocationBinType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseLocationBinTypes(itemType);
        }

        public void DeleteFromRepository(WarehouseLocationBinType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {//if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseLocationBinType deletedWarehouseLocationBinType = (from q in context.WarehouseLocationBinTypes
                                          where q.WarehouseLocationBinTypeID == itemType.WarehouseLocationBinTypeID
                                          select q).FirstOrDefault();
                if (deletedWarehouseLocationBinType != null)
                {
                    context.DeleteObject(deletedWarehouseLocationBinType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetWarehouseLocationBinTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetWarehouseLocationBinTypeEntityState(WarehouseLocationBinType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
