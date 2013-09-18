using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.PlantDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;


namespace XERP.Server.Service.PlantService
{
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PlantDataService : DataService<PlantEntities>
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
                case "Plants":
                    Plant Plant = new Plant();
                    return Plant.GetMetaData().AsQueryable();
                case "PlantTypes":
                    PlantType PlantType = new PlantType();
                    return PlantType.GetMetaData().AsQueryable();
                case "PlantCodes":
                    PlantCode PlantCode = new PlantCode();
                    return PlantCode.GetMetaData().AsQueryable();
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
        public IQueryable<Plant> RefreshPlant(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.PlantDAL.DALUtility dalUtility = new DALUtility();
            var context = new PlantEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.Plants
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<PlantType> RefreshPlantType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.PlantDAL.DALUtility dalUtility = new DALUtility();
            var context = new PlantEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.PlantTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<PlantCode> RefreshPlantCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.PlantDAL.DALUtility dalUtility = new DALUtility();
            var context = new PlantEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.PlantCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("PlantTypes")]
        public void OnChangePlantTypes(PlantType PlantType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.PlantDAL.DALUtility dalUtility = new DALUtility();
                var context = new PlantEntities(dalUtility.EntityConectionString);
                context.Plants.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = PlantType.CompanyID;
                string typeID = PlantType.PlantTypeID;
                string sqlstring = "UPDATE Plants SET PlantTypeID = null WHERE CompanyID = '" + companyID + "' and PlantTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("PlantCodes")]
        public void OnChangePlantTypes(PlantCode PlantCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.PlantDAL.DALUtility dalUtility = new DALUtility();
                var context = new PlantEntities(dalUtility.EntityConectionString);
                context.Plants.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = PlantCode.CompanyID;
                string codeID = PlantCode.PlantCodeID;
                string sqlstring = "UPDATE Plants SET PlantCodeID = null WHERE CompanyID = '" + companyID + "' and PlantCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        protected override PlantEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.PlantDAL.DALUtility dalUtility = new DALUtility();
                var context = new PlantEntities(dalUtility.EntityConectionString);

                //test it...
                //GetMetaData("Plants");
                //IQueryable<Plant> PlantQuery = (from c in context.Plants
                                                                //select c);

                //foreach (Plant cc in PlantQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //Plant Plant = new Plant();
                //IQueryable<Plant> PlantQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

