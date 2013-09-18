using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain
{
    public class WarehouseLocationTypeSingletonRepository
    {
        private WarehouseLocationTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }

        private static WarehouseLocationTypeSingletonRepository _instance;
        public static WarehouseLocationTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseLocationTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypes(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypes(WarehouseLocationType itemTypeQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseLocationTypes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.WarehouseLocationTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.WarehouseLocationTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<WarehouseLocationType> GetWarehouseLocationTypeByID(string itemTypeID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationTypes
                               where q.WarehouseLocationTypeID == itemTypeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationType> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseLocationType>("RefreshWarehouseLocationType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseLocationType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(WarehouseLocationType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseLocationTypes(itemType);
        }

        public void DeleteFromRepository(WarehouseLocationType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {//if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseLocationType deletedWarehouseLocationType = (from q in context.WarehouseLocationTypes
                                          where q.WarehouseLocationTypeID == itemType.WarehouseLocationTypeID
                                          select q).FirstOrDefault();
                if (deletedWarehouseLocationType != null)
                {
                    context.DeleteObject(deletedWarehouseLocationType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetWarehouseLocationTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetWarehouseLocationTypeEntityState(WarehouseLocationType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
