using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.CompanyDAL;
using System.Data.Metadata.Edm;
using System.ServiceModel.Web;
using System.Diagnostics;
using System.Collections.Generic;
using ExtensionMethods;

namespace XERP.Server.Service.CompanyService
{
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
                    Company company = new Company();
                    return company.GetMetaData().AsQueryable();
                case "CompanyTypes":
                    CompanyType companyType = new CompanyType();
                    return companyType.GetMetaData().AsQueryable();
                case "CompanyCodes":
                    CompanyCode companyCode = new CompanyCode();
                    return companyCode.GetMetaData().AsQueryable();
                default: //no table exists for the given tablename given...
                    List<Temp> tempList = new List<Temp>();
                    Temp temp = new Temp();
                    temp.ID = 0;
                    temp.Int_1 = 0;
                    temp.Bool_1 = true; //bool_1 will flag it as an error...
                    temp.Name = "Error";
                    temp.ShortChar_1 = "Table " + tableName + " Is Not A Valid Table Within The Given Entity Collection, Or Meta Data Is Not Publc For The Given Table Name";
                    tempList.Add(temp);
                    return tempList.AsQueryable();
            }
        }

        protected override CompanyEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
                var context = new CompanyEntities(dalUtility.EntityConectionString);
               
                //test it...
                //IQueryable<Company> companyQuery = (from c in context.Companies
                //                                   select c);

                //foreach (Company cc in CompanyQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //Company company = new Company();
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }   
    } 
}

