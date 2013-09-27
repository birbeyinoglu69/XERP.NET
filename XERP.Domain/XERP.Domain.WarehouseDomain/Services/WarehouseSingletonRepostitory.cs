using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain
{
    public class WarehouseSingletonRepository
    {
        private WarehouseSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }
        
        private static WarehouseSingletonRepository _instance;
        public static WarehouseSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<Warehouse> GetWarehouses(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Warehouses
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<Warehouse> GetWarehouses(Warehouse itemQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.Warehouses
                              where q.CompanyID == companyID
                             select q; 
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(itemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.WarehouseTypeID))
                queryResult = queryResult.Where(q => q.WarehouseTypeID.StartsWith(itemQuerryObject.WarehouseTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.WarehouseCodeID))
                queryResult = queryResult.Where(q => q.WarehouseCodeID.StartsWith(itemQuerryObject.WarehouseCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<Warehouse> GetWarehouseByID(string itemID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Warehouses
                          where q.WarehouseID == itemID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<Warehouse> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<Warehouse>("RefreshWarehouse").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(Warehouse item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(Warehouse item)
        {
            item.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouses(item);
        }

        public void DeleteFromRepository(Warehouse item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                //if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                Warehouse deletedWarehouse = (from q in context.Warehouses
                                          where q.WarehouseID == item.WarehouseID
                                          select q).FirstOrDefault();
                if (deletedWarehouse != null)
                {
                    context.DeleteObject(deletedWarehouse);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetWarehouseEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetWarehouseEntityState(Warehouse item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
