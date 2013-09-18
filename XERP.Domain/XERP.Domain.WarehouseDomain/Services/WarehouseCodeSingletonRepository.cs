using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain.Services
{
    public class WarehouseCodeSingletonRepository
    {
        private WarehouseCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }

        private static WarehouseCodeSingletonRepository _instance;
        public static WarehouseCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseCode> GetWarehouseCodes(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseCode> GetWarehouseCodes(WarehouseCode itemCodeQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseCodes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith( itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.WarehouseCodeID))

                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.WarehouseCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<WarehouseCode> GetWarehouseCodeByID(string itemCodeID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseCodes
                               where q.WarehouseCodeID == itemCodeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseCode> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseCode>("RefreshWarehouseCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(WarehouseCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseCodes( itemCode);
        }

        public void DeleteFromRepository(WarehouseCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
            {//if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseCode deletedWarehouseCode = (from q in context.WarehouseCodes
                                          where q.WarehouseCodeID == itemCode.WarehouseCodeID
                                          select q).FirstOrDefault();
                if (deletedWarehouseCode != null)
                {
                    context.DeleteObject(deletedWarehouseCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetWarehouseCodeEntityState( itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach( itemCode);
            }
        }

        public EntityStates GetWarehouseCodeEntityState(WarehouseCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
                return _repositoryContext.GetEntityDescriptor( itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}