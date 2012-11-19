using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain.Services;

namespace XERP.Domain.CompanyDomain.Services
{
    public class CompanyCodeSingletonRepository
    {
        private CompanyCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new CompanyEntities(_rootUri);
        }

        private static CompanyCodeSingletonRepository _instance;
        public static CompanyCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CompanyCodeSingletonRepository();
                return _instance;
            }
        }

        private Uri _rootUri;
        private CompanyEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<CompanyCode> GetCompanyCodes()
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyCodes
                               select q);
            return queryResult;
        }

        public IEnumerable<CompanyCode> GetCompanyCodes(CompanyCode itemCodeQuerryObject)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.CompanyCodes
                              select q;
            if (!string.IsNullOrEmpty(itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith(itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty(itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemCodeQuerryObject.CompanyCodeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemCodeQuerryObject.CompanyCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<CompanyCode> GetCompanyCodeByID(string itemCodeID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyCodes
                               where q.CompanyCodeID == itemCodeID
                               select q);
            return queryResult;
        }

        public IEnumerable<CompanyCode> Refresh(string autoIDs)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<CompanyCode>("RefreshCompanyCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(CompanyCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(CompanyCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanyCodes(itemCode);
        }

        public void DeleteFromRepository(CompanyCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {//if it exists in the db delete it from the db
                CompanyEntities context = new CompanyEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                CompanyCode deletedCompanyCode = (from q in context.CompanyCodes
                                          where q.CompanyCodeID == itemCode.CompanyCodeID
                                          select q).SingleOrDefault();
                if (deletedCompanyCode != null)
                {
                    context.DeleteObject(deletedCompanyCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetCompanyCodeEntityState(itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach(itemCode);
            }
        }

        public EntityStates GetCompanyCodeEntityState(CompanyCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
                return _repositoryContext.GetEntityDescriptor(itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}