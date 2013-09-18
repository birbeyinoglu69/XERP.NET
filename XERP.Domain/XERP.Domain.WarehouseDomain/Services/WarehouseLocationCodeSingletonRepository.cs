using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain.Services
{
    public class WarehouseLocationCodeSingletonRepository
    {
        private WarehouseLocationCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }

        private static WarehouseLocationCodeSingletonRepository _instance;
        public static WarehouseLocationCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseLocationCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodes(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodes(WarehouseLocationCode itemCodeQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseLocationCodes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith( itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.WarehouseLocationCodeID))

                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.WarehouseLocationCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<WarehouseLocationCode> GetWarehouseLocationCodeByID(string itemCodeID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationCodes
                               where q.WarehouseLocationCodeID == itemCodeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationCode> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseLocationCode>("RefreshWarehouseLocationCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseLocationCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(WarehouseLocationCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseLocationCodes( itemCode);
        }

        public void DeleteFromRepository(WarehouseLocationCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
            {//if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseLocationCode deletedWarehouseLocationCode = (from q in context.WarehouseLocationCodes
                                          where q.WarehouseLocationCodeID == itemCode.WarehouseLocationCodeID
                                          select q).FirstOrDefault();
                if (deletedWarehouseLocationCode != null)
                {
                    context.DeleteObject(deletedWarehouseLocationCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetWarehouseLocationCodeEntityState( itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach( itemCode);
            }
        }

        public EntityStates GetWarehouseLocationCodeEntityState(WarehouseLocationCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
                return _repositoryContext.GetEntityDescriptor( itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}