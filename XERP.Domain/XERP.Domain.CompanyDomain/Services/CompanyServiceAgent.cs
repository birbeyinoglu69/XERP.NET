using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using XERP.Domain.CompanyDomain.CompanyDataService;

namespace XERP.Domain.CompanyDomain.Services
{
    public class CompanyServiceAgent : XERP.Domain.CompanyDomain.Services.ICompanyServiceAgent 
    {
        #region Initialize Service
        public CompanyServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new CompanyEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private CompanyEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool CompanyRepositoryIsDirty()
        {
            return CompanySingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool CompanyTypeRepositoryIsDirty()
        {
            return CompanyTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool CompanyCodeRepositoryIsDirty()
        {
            return CompanyCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 

        public IEnumerable<CompanyType> GetCompanyTypesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from types in _context.CompanyTypes
                                select types);
            return queryResult;
        }

        public IEnumerable<CompanyCode> GetCompanyCodesReadOnly()
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from codes in _context.CompanyCodes
                                select codes);
            return queryResult;
        }

        public bool CompanyExists(string itemID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Companies
                               where q.CompanyID == itemID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool CompanyTypeExists(string itemTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.CompanyTypes
                               where q.CompanyTypeID == itemTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool CompanyCodeExists(string itemCodeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.CompanyCodes
                               where q.CompanyCodeID == itemCodeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {   //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }
        #endregion Read Only Methods  No Repository Required

        #region Company Repository CRUD
        public IEnumerable<Company> RefreshCompany(string autoIDs)
        {
            return CompanySingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<Company> GetCompanies()
        {
            return CompanySingletonRepository.Instance.GetCompanies();
        }

        public IEnumerable<Company> GetCompanies(Company itemQuerryObject)
        {
            return CompanySingletonRepository.Instance.GetCompanies(itemQuerryObject);
        }

        public IEnumerable<Company> GetCompanyByID(string itemID)
        {
            return CompanySingletonRepository.Instance.GetCompanyByID(itemID);
        }

        public void CommitCompanyRepository()
        {
            CompanySingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyRepository(Company item)
        {
            CompanySingletonRepository.Instance.UpdateRepository(item);
        }

        public void AddToCompanyRepository(Company item)
        {
            CompanySingletonRepository.Instance.AddToRepository(item);
        }

        public void DeleteFromCompanyRepository(Company item)
        {
            CompanySingletonRepository.Instance.DeleteFromRepository(item);
        }

        public EntityStates GetCompanyEntityState(Company item)
        {
            return CompanySingletonRepository.Instance.GetCompanyEntityState(item);
        }
        #endregion Company Repository CRUD

        #region CompanyType Repository CRUD
        public IEnumerable<CompanyType> RefreshCompanyType(string autoIDs)
        {
            return CompanyTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<CompanyType> GetCompanyTypes()
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypes();
        }

        public IEnumerable<CompanyType> GetCompanyTypes(CompanyType itemTypeQuerryObject)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypes(itemTypeQuerryObject);
        }

        public IEnumerable<CompanyType> GetCompanyTypeByID(string itemTypeID)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypeByID(itemTypeID);
        }
        public void CommitCompanyTypeRepository()
        {
            CompanyTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyTypeRepository(CompanyType itemType)
        {
            CompanyTypeSingletonRepository.Instance.UpdateRepository(itemType);
        }

        public void AddToCompanyTypeRepository(CompanyType itemType)
        {
            CompanyTypeSingletonRepository.Instance.AddToRepository(itemType);
        }

        public void DeleteFromCompanyTypeRepository(CompanyType itemType)
        {
            CompanyTypeSingletonRepository.Instance.DeleteFromRepository(itemType);
        }

        public EntityStates GetCompanyTypeEntityState(CompanyType itemType)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypeEntityState(itemType);
        }
        #endregion CompanyType Repository CRUD

        #region CompanyCode Repository CRUD
        public IEnumerable<CompanyCode> RefreshCompanyCode(string autoIDs)
        {
            return CompanyCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<CompanyCode> GetCompanyCodes()
        {
            return CompanyCodeSingletonRepository.Instance.GetCompanyCodes();
        }

        public IEnumerable<CompanyCode> GetCompanyCodes(CompanyCode itemCodeQuerryObject)
        {
            return CompanyCodeSingletonRepository.Instance.GetCompanyCodes(itemCodeQuerryObject);
        }

        public IEnumerable<CompanyCode> GetCompanyCodeByID(string itemCodeID)
        {
            return CompanyCodeSingletonRepository.Instance.GetCompanyCodeByID(itemCodeID);
        }
        public void CommitCompanyCodeRepository()
        {
            CompanyCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyCodeRepository(CompanyCode itemCode)
        {
            CompanyCodeSingletonRepository.Instance.UpdateRepository(itemCode);
        }

        public void AddToCompanyCodeRepository(CompanyCode itemCode)
        {
            CompanyCodeSingletonRepository.Instance.AddToRepository(itemCode);
        }

        public void DeleteFromCompanyCodeRepository(CompanyCode itemCode)
        {
            CompanyCodeSingletonRepository.Instance.DeleteFromRepository(itemCode);
        }

        public EntityStates GetCompanyCodeEntityState(CompanyCode itemCode)
        {
            return CompanyCodeSingletonRepository.Instance.GetCompanyCodeEntityState(itemCode);
        }
        #endregion CompanyCode Repository CRUD
    }
}
