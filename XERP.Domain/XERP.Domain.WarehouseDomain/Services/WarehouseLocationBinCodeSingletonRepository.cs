using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.WarehouseDomain.WarehouseDataService;
using XERP.Domain.WarehouseDomain.Services;

namespace XERP.Domain.WarehouseDomain.Services
{
    public class WarehouseLocationBinCodeSingletonRepository
    {
        private WarehouseLocationBinCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new WarehouseEntities(_rootUri);
        }

        private static WarehouseLocationBinCodeSingletonRepository _instance;
        public static WarehouseLocationBinCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WarehouseLocationBinCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private WarehouseEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodes(string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationBinCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodes(WarehouseLocationBinCode itemCodeQuerryObject, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.WarehouseLocationBinCodes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith( itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.WarehouseLocationBinCodeID))

                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.WarehouseLocationBinCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<WarehouseLocationBinCode> GetWarehouseLocationBinCodeByID(string itemCodeID, string companyID)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.WarehouseLocationBinCodes
                               where q.WarehouseLocationBinCodeID == itemCodeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<WarehouseLocationBinCode> Refresh(string autoIDs)
        {
            _repositoryContext = new WarehouseEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<WarehouseLocationBinCode>("RefreshWarehouseLocationBinCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(WarehouseLocationBinCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(WarehouseLocationBinCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToWarehouseLocationBinCodes( itemCode);
        }

        public void DeleteFromRepository(WarehouseLocationBinCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
            {//if it exists in the db delete it from the db
                WarehouseEntities context = new WarehouseEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                WarehouseLocationBinCode deletedWarehouseLocationBinCode = (from q in context.WarehouseLocationBinCodes
                                          where q.WarehouseLocationBinCodeID == itemCode.WarehouseLocationBinCodeID
                                          select q).FirstOrDefault();
                if (deletedWarehouseLocationBinCode != null)
                {
                    context.DeleteObject(deletedWarehouseLocationBinCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetWarehouseLocationBinCodeEntityState( itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach( itemCode);
            }
        }

        public EntityStates GetWarehouseLocationBinCodeEntityState(WarehouseLocationBinCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
                return _repositoryContext.GetEntityDescriptor( itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}