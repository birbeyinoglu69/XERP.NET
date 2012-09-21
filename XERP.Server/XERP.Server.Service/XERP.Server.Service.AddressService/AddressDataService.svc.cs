using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.AddressDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;

namespace XERP.Server.Service.AddressService
{
    public class AddressDataService : DataService<AddressEntities>
    {
        // This method is called only once to initialize service-wide policies.
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
                case "Addresses":
                    Address Address = new Address();
                    return Address.GetMetaData().AsQueryable();
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
        public IQueryable<Address> RefreshAddress(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.AddressDAL.DALUtility dalUtility = new DALUtility();
            var context = new AddressEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.Addresses
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        protected override AddressEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.AddressDAL.DALUtility dalUtility = new DALUtility();
                var context = new AddressEntities(dalUtility.EntityConectionString);

                //test it...
                //GetMetaData("Addresses");
                //IQueryable<Address> AddressQuery = (from c in context.Addresses
                //                                    select c);

                //foreach (Address cc in AddressQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //Address Address = new Address();
                //IQueryable<Address> AddressQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

