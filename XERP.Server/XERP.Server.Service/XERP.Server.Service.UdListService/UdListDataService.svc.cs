using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.UdListDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;

namespace XERP.Server.Service.UdListService
{
    public class UdListDataService : DataService<UdListEntities>
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
                case "UdLists":
                    UdList udList = new UdList();
                    return udList.GetMetaData().AsQueryable();
                case "UdListItems":
                    UdListItem udListItem = new UdListItem();
                    return udListItem.GetMetaData().AsQueryable();
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
        public IQueryable<UdList> RefreshUdList(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.UdListDAL.DALUtility dalUtility = new DALUtility();
            var context = new UdListEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.UdLists
                               where query.Contains(q.AutoID)
                               select q);
            return queryResult;
        }

        [WebGet]
        public IQueryable<UdListItem> RefreshUdListItem(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.UdListDAL.DALUtility dalUtility = new DALUtility();
            var context = new UdListEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.UdListItems
                               where query.Contains(q.AutoID)
                               select q);
            return queryResult;
        }

        protected override UdListEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.UdListDAL.DALUtility dalUtility = new DALUtility();
                var context = new UdListEntities(dalUtility.EntityConectionString);

                //test it...
                //GetMetaData("UdLists");
                //IQueryable<UdList> UdListQuery = (from c in context.UdLists
                //                                    select c);

                //foreach (UdList cc in UdListQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //UdList UdList = new UdList();
                //IQueryable<UdList> UdListQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}



