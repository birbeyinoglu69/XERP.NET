using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.CompanyDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;

namespace XERP.Server.Service.CompanyService
{
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]   
    public class CompanyDataService : DataService< CompanyEntities >
    {
        //private CompanyEntities _context;
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            //I noticed when setting page size it would always limit my expand payload to 1
            //I have elected to disable paging...
            //config.SetEntitySetPageSize("*", 50);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }

        //The procedure below uses the Temp table as following...
        //ID item count key id...
        //Name MetaData FieldName
        //Int_1 String Field Max Length
        //Bool_1 Error Flag
        [WebGet]
        public IQueryable<Temp> GetMetaData(string tableName)
        {
            switch (tableName)
            {
                case "Companies":
                    Company item = new Company();
                    return item.GetMetaData().AsQueryable();
                case "CompanyTypes":
                    CompanyType itemType = new CompanyType();
                    return itemType.GetMetaData().AsQueryable();
                case "CompanyCodes":
                    CompanyCode itemCode = new CompanyCode();
                    return itemCode.GetMetaData().AsQueryable();
                default: //no table exists for the given tablename given...
                    List<Temp> tempList = new List<Temp>();
                    Temp temp = new Temp();
                    temp.ID = 0;
                    temp.Int_1 = 0;
                    temp.Bool_1 = true; //bool_1 will flag it as an error...
                    temp.Name = "Error";
                    temp.ShortChar_1 = "Table " + tableName + " Is Not A Valid Table Within The Given Entity Collection, Or Meta Data Was Not Defined For The Given Table Name";
                    tempList.Add(temp);
                    return tempList.AsQueryable();
            }
        }

        [WebGet]
        public IQueryable<Company> RefreshCompany(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
            var context = new CompanyEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.Companies
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<CompanyType> RefreshCompanyType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
            var context = new CompanyEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.CompanyTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<CompanyCode> RefreshCompanyCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
            var context = new CompanyEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.CompanyCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("CompanyTypes")]
        public void OnChangeCompanyTypes(CompanyType itemType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
                var context = new CompanyEntities(dalUtility.EntityConectionString);
                context.Companies.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string typeID = itemType.CompanyTypeID;
                string sqlstring = "UPDATE Companies SET CompanyTypeID = null WHERE CompanyTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("CompanyCodes")]
        public void OnChangeCompanyCodes(CompanyCode itemCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
                var context = new CompanyEntities(dalUtility.EntityConectionString);
                context.Companies.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string codeID = itemCode.CompanyCodeID;
                string sqlstring = "UPDATE Companies SET CompanyCodeID = null Where CompanyCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        protected override CompanyEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
                var context = new CompanyEntities(dalUtility.EntityConectionString);
               
                //test it...
                //GetMetaData("Companies");
                //IQueryable<Company> itemQuerry = (from c in context.Companies
                //                                    select c);

                //foreach (Company cc in CompanyQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //Company item = new Company();
                //IQueryable<Company> itemQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }   
    } 
}

