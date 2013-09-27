using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain
{
    public class WarehouseLocationSingletonRepository
    {
        private WarehouseLocationSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }
        
        private static WarehouseLocationSingletonRepository _instance;
        public static WarehouseLocationSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseLocationSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocations(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocations.Expand("Warehouse/Plant")
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocations(WarehouseLocation itemQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseLocations.Expand("Warehouse/Plant")
                              where q.CompanyID == companyID
                             select q; 
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(itemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.WarehouseLocationTypeID))
                queryResult = queryResult.Where(q => q.WarehouseLocationTypeID.StartsWith(itemQuerryObject.WarehouseLocationTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.WarehouseLocationCodeID))
                queryResult = queryResult.Where(q => q.WarehouseLocationCodeID.StartsWith(itemQuerryObject.WarehouseLocationCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<WarehouseLocation> GetWarehouseLocationByID(string itemID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocations.Expand("Warehouse/Plant")
                          where q.WarehouseLocationID == itemID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocation> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseLocation>("RefreshWarehouseLocation").AddQueryOption("autoIDs", "'" + autoIDs + "'").Expand("Warehouse/Plant");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseLocation item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(WarehouseLocation item)
        {
            item.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseLocations(item);
        }

        public void DeleteFromRepository(WarehouseLocation item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                //if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseLocation deletedWarehouseLocation = (from q in context.WarehouseLocations
                                          where q.WarehouseLocationID == item.WarehouseLocationID
                                          select q).FirstOrDefault();
                if (deletedWarehouseLocation != null)
                {
                    context.DeleteObject(deletedWarehouseLocation);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetWarehouseLocationEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetWarehouseLocationEntityState(WarehouseLocation item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
