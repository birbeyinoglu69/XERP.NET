using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain
{
    public class WarehouseLocationBinSingletonRepository
    {
        private WarehouseLocationBinSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }
        
        private static WarehouseLocationBinSingletonRepository _instance;
        public static WarehouseLocationBinSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseLocationBinSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBins(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationBins.Expand("WarehouseLocation/Warehouse/Plant")
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBins(WarehouseLocationBin itemQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseLocationBins.Expand("WarehouseLocation/Warehouse/Plant")
                              where q.CompanyID == companyID
                             select q; 
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(itemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.WarehouseLocationBinTypeID))
                queryResult = queryResult.Where(q => q.WarehouseLocationBinTypeID.StartsWith(itemQuerryObject.WarehouseLocationBinTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.WarehouseLocationBinCodeID))
                queryResult = queryResult.Where(q => q.WarehouseLocationBinCodeID.StartsWith(itemQuerryObject.WarehouseLocationBinCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<WarehouseLocationBin> GetWarehouseLocationBinByID(string itemID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationBins.Expand("WarehouseLocation/Warehouse/Plant")
                          where q.WarehouseLocationBinID == itemID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBin> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseLocationBin>("RefreshWarehouseLocationBin").AddQueryOption("autoIDs", "'" + autoIDs + "'").Expand("WarehouseLocation/Warehouse/Plant");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseLocationBin item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(WarehouseLocationBin item)
        {
            item.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseLocationBins(item);
        }

        public void DeleteFromRepository(WarehouseLocationBin item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                //if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseLocationBin deletedWarehouseLocationBin = (from q in context.WarehouseLocationBins
                                          where q.WarehouseLocationBinID == item.WarehouseLocationBinID
                                          select q).FirstOrDefault();
                if (deletedWarehouseLocationBin != null)
                {
                    context.DeleteObject(deletedWarehouseLocationBin);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetWarehouseLocationBinEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetWarehouseLocationBinEntityState(WarehouseLocationBin item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
