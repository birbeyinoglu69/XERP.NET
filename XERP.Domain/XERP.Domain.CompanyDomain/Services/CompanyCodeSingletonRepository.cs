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
                {
                    _instance = new CompanyCodeSingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private CompanyEntities _repositoryContext;

        public IEnumerable<CompanyCode> GetCompanyCodes()
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyCodes
                               select q);
            return queryResult;
        }

        public IEnumerable<CompanyCode> GetCompanyCodes(CompanyCode companyCodeQuerryObject)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.CompanyCodes
                              select q;

            if (!string.IsNullOrEmpty(companyCodeQuerryObject.Code))
            {
                queryResult = queryResult.Where(q => q.Code.StartsWith(companyCodeQuerryObject.Code.ToString()));
            }

            if (!string.IsNullOrEmpty(companyCodeQuerryObject.Description))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyCodeQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(companyCodeQuerryObject.CompanyCodeID))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(companyCodeQuerryObject.CompanyCodeID.ToString()));
            }

            return queryResult;
        }


        public IEnumerable<CompanyCode> GetCompanyCodeByID(string companyCodeID)
        {
            _repositoryContext = new CompanyEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.CompanyCodes
                               where q.CompanyCodeID == companyCodeID
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

        public void UpdateRepository(CompanyCode companyCode)
        {
            if (_repositoryContext.GetEntityDescriptor(companyCode) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(companyCode);
            }
        }

        public void AddToRepository(CompanyCode companyCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToCompanyCodes(companyCode);
        }

        public void DeleteFromRepository(CompanyCode companyCode)
        {
            if (_repositoryContext.GetEntityDescriptor(companyCode) != null)
            {
                //if it exists in the db delete it from the db
                CompanyEntities context = new CompanyEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                CompanyCode deletedCompanyCode = (from q in context.CompanyCodes
                                          where q.CompanyCodeID == companyCode.CompanyCodeID
                                          select q).SingleOrDefault();
                if (deletedCompanyCode != null)
                {
                    context.DeleteObject(deletedCompanyCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetCompanyCodeEntityState(companyCode) != EntityStates.Detached)
                {
                    _repositoryContext.Detach(companyCode);
                }
            }
        }

        public EntityStates GetCompanyCodeEntityState(CompanyCode companyCode)
        {
            if (_repositoryContext.GetEntityDescriptor(companyCode) != null)
            {
                return _repositoryContext.GetEntityDescriptor(companyCode).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }
    }
}