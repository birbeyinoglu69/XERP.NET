using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using XERP.Server.DAL.WarehouseDAL;
using System.ServiceModel.Web;
using System.Collections.Generic;
using ExtensionMethods;


namespace XERP.Server.Service.WarehouseService
{
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WarehouseDataService : DataService<WarehouseEntities>
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
                case "Warehouses":
                    Warehouse Warehouse = new Warehouse();
                    return Warehouse.GetMetaData().AsQueryable();
                case "WarehouseTypes":
                    WarehouseType WarehouseType = new WarehouseType();
                    return WarehouseType.GetMetaData().AsQueryable();
                case "WarehouseCodes":
                    WarehouseCode WarehouseCode = new WarehouseCode();
                    return WarehouseCode.GetMetaData().AsQueryable();
                case "WarehouseLocationBins":
                    WarehouseLocationBin WarehouseLocationBin = new WarehouseLocationBin();
                    return WarehouseLocationBin.GetMetaData().AsQueryable();
                case "WarehouseLocationBinTypes":
                    WarehouseLocationBinType WarehouseLocationBinType = new WarehouseLocationBinType();
                    return WarehouseLocationBinType.GetMetaData().AsQueryable();
                case "WarehouseLocationBinCodes":
                    WarehouseLocationBinCode WarehouseLocationBinCode = new WarehouseLocationBinCode();
                    return WarehouseLocationBinCode.GetMetaData().AsQueryable();
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

        #region Warehouse Specific Services...
        [WebGet]
        public IQueryable<Warehouse> RefreshWarehouse(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.Warehouses
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<WarehouseType> RefreshWarehouseType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<WarehouseCode> RefreshWarehouseCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("WarehouseTypes")]
        public void OnChangeWarehouseTypes(WarehouseType warehouseType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);
                context.Warehouses.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = warehouseType.CompanyID;
                string typeID = warehouseType.WarehouseTypeID;
                string sqlstring = "UPDATE Warehouses SET WarehouseTypeID = null WHERE CompanyID = '" + companyID + "' and WarehouseTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("WarehouseCodes")]
        public void OnChangeWarehouseTypes(WarehouseCode warehouseCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);
                context.Warehouses.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = warehouseCode.CompanyID;
                string codeID = warehouseCode.WarehouseCodeID;
                string sqlstring = "UPDATE Warehouses SET WarehouseCodeID = null WHERE CompanyID = '" + companyID + "' and WarehouseCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }
        #endregion Warehouse Specific Services...

        #region WarehouseLocation Specific Services...
        [WebGet]
        public IQueryable<WarehouseLocation> RefreshWarehouseLocation(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseLocations
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<WarehouseLocationType> RefreshWarehouseLocationType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseLocationTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<WarehouseLocationCode> RefreshWarehouseLocationCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseLocationCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("WarehouseLocationTypes")]
        public void OnChangeWarehouseLocationTypes(WarehouseLocationType warehouseType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);
                context.WarehouseLocations.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = warehouseType.CompanyID;
                string typeID = warehouseType.WarehouseLocationTypeID;
                string sqlstring = "UPDATE WarehouseLocations SET WarehouseLocationTypeID = null WHERE CompanyID = '" + companyID + "' and WarehouseLocationTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("WarehouseLocationCodes")]
        public void OnChangeWarehouseLocationTypes(WarehouseLocationCode warehouseCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);
                context.WarehouseLocations.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = warehouseCode.CompanyID;
                string codeID = warehouseCode.WarehouseLocationCodeID;
                string sqlstring = "UPDATE WarehouseLocations SET WarehouseLocationCodeID = null WHERE CompanyID = '" + companyID + "' and WarehouseLocationCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }
        #endregion WarehouseLocation Specific Services...

        #region WarehouseLocationBin Specific Services...
        [WebGet]
        public IQueryable<WarehouseLocationBin> RefreshWarehouseLocationBin(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseLocationBins
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<WarehouseLocationBinType> RefreshWarehouseLocationBinType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseLocationBinTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<WarehouseLocationBinCode> RefreshWarehouseLocationBinCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
            var context = new WarehouseEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.WarehouseLocationBinCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("WarehouseLocationBinTypes")]
        public void OnChangeWarehouseLocationBinTypes(WarehouseLocationBinType warehouseType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);
                context.WarehouseLocationBins.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = warehouseType.CompanyID;
                string typeID = warehouseType.WarehouseLocationBinTypeID;
                string sqlstring = "UPDATE WarehouseLocationBins SET WarehouseLocationBinTypeID = null WHERE CompanyID = '" + companyID + "' and WarehouseLocationBinTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("WarehouseLocationBinCodes")]
        public void OnChangeWarehouseLocationBinTypes(WarehouseLocationBinCode warehouseCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);
                context.WarehouseLocationBins.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = warehouseCode.CompanyID;
                string codeID = warehouseCode.WarehouseLocationBinCodeID;
                string sqlstring = "UPDATE WarehouseLocationBins SET WarehouseLocationBinCodeID = null WHERE CompanyID = '" + companyID + "' and WarehouseLocationBinCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }
        #endregion WarehouseLocationBin Specific Services...

        protected override WarehouseEntities CreateDataSource()
        {
            try
            {
                XERP.Server.DAL.WarehouseDAL.DALUtility dalUtility = new DALUtility();
                var context = new WarehouseEntities(dalUtility.EntityConectionString);

                //test it...
                //GetMetaData("Warehouses");
                //IQueryable<Warehouse> WarehouseQuery = (from c in context.Warehouses
                //                                                select c);

                //foreach (Warehouse cc in WarehouseQuery)
                //{
                //    string s = cc.Name.ToString();
                //}
                //Warehouse Warehouse = new Warehouse();
                //IQueryable<Warehouse> WarehouseQuery = Refresh("5,6");
                return context;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}

