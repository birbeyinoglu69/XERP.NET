using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using System.Reflection;
//using System.Linq.Dynamic;
using System.Collections.ObjectModel;
using XERP.Domain.CompanyDomain.CompanyDataService;
using XERP.Domain.CompanyDomain;
using XERP.Domain.CompanyDomain.Services;
using XERP.Domain.ClientModels;
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

        //we use a temp table in instances where data base schema is not required...
        //we use the the temp table db schema as the conduit from client to server...
        //we then traspose the temp table in to a more meaningful client model...
        public List<EntityMetaData> CompanyMetaDataList
        {
            get 
            {
                List<EntityMetaData> entityMetaDataList = new List<EntityMetaData>();

                foreach(Temp temp in GetMetaData("Companies"))
                {
                    EntityMetaData entityMetaData = new EntityMetaData();
                    entityMetaData.ID = temp.ID;
                    entityMetaData.FieldName = temp.Name;
                    entityMetaData.FieldType = temp.ShortChar_1;
                    entityMetaData.MaxLength = temp.Int_1;
                }
                return entityMetaDataList; 
            } 
        }
        
        #endregion Properties

        #region Read Only Methods  No Repository Required
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

        public bool CompanyExists(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Companies
                           where q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool CompanyTypeIsUsed(string companyTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.Companies
                               where q.CompanyTypeID == companyTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public bool CompanyTypeExists(string companyTypeID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.CompanyTypes
                               where q.CompanyTypeID == companyTypeID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
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

        public IEnumerable<Company> GetCompanies(Company companyQuerryObject)
        {
            return CompanySingletonRepository.Instance.GetCompanies(companyQuerryObject);
        }

        public IEnumerable<Company> GetCompanyByID(string companyID)
        {
            return CompanySingletonRepository.Instance.GetCompanyByID(companyID);
        }

        public void CommitCompanyRepository()
        {
            CompanySingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyRepository(Company company)
        {
            CompanySingletonRepository.Instance.UpdateRepository(company);
        }

        public void AddToCompanyRepository(Company company)
        {
            CompanySingletonRepository.Instance.AddToRepository(company);
        }

        public void DeleteFromCompanyRepository(Company company)
        {
            CompanySingletonRepository.Instance.DeleteFromRepository(company);
        }

        public EntityStates GetCompanyEntityState(Company company)
        {
            return CompanySingletonRepository.Instance.GetCompanyEntityState(company);
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

        public IEnumerable<CompanyType> GetCompanyTypes(CompanyType companyTypeQuerryObject)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypes(companyTypeQuerryObject);
        }

        public IEnumerable<CompanyType> GetCompanyTypeByID(string companyTypeID)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypeByID(companyTypeID);
        }
        public void CommitCompanyTypeRepository()
        {
            CompanyTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateCompanyTypeRepository(CompanyType companyType)
        {
            CompanyTypeSingletonRepository.Instance.UpdateRepository(companyType);
        }

        public void AddToCompanyTypeRepository(CompanyType companyType)
        {
            CompanyTypeSingletonRepository.Instance.AddToRepository(companyType);
        }

        public void DeleteFromCompanyTypeRepository(CompanyType companyType)
        {
            CompanyTypeSingletonRepository.Instance.DeleteFromRepository(companyType);
        }

        public EntityStates GetCompanyTypeEntityState(CompanyType companyType)
        {
            return CompanyTypeSingletonRepository.Instance.GetCompanyTypeEntityState(companyType);
        }

        #endregion CompanyType Repository CRUD
    }
}
