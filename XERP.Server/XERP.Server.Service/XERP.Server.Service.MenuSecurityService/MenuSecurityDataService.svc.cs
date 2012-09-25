using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using XERP.Server.DAL.MenuSecurityDAL;
using ExtensionMethods;

namespace XERP.Server.Service.MenuSecurityService
{
    public class MenuSecurityDataService : DataService<MenuSecurityEntities>
    {
        //private MenuSecurityEntities _context;

        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            //I noticed when setting page size it would always limit my expand payload to 1
            //I have elected to disable paging...
            //config.SetEntitySetPageSize("*", 50);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }

        [WebGet]
        public IQueryable<Temp> GetMetaData(string tableName)
        {
            switch (tableName)
            {
                case "MenuItems":
                    MenuItem menuItem = new MenuItem();
                    return menuItem.GetMetaData().AsQueryable();
                case "MenuItemTypes":
                    MenuItemType menuItemType = new MenuItemType();
                    return menuItemType.GetMetaData().AsQueryable();
                case "MenuItemCodes":
                    MenuItemCode menuItemCode = new MenuItemCode();
                    return menuItemCode.GetMetaData().AsQueryable();
                case "ExecutablePrograms":
                    ExecutableProgram executablePrograms = new ExecutableProgram();
                    return executablePrograms.GetMetaData().AsQueryable();
                case "ExecutableProgramTypes":
                    ExecutableProgramType executableProgramType = new ExecutableProgramType();
                    return executableProgramType.GetMetaData().AsQueryable();
                case "ExecutableProgramCodes":
                    ExecutableProgramCode executableProgramCode = new ExecutableProgramCode();
                    return executableProgramCode.GetMetaData().AsQueryable();
                case "MenuSecurities":
                    MenuSecurity menuSecurity = new MenuSecurity();
                    return menuSecurity.GetMetaData().AsQueryable();
                case "SecurityGroups":
                    SecurityGroup securityGroup = new SecurityGroup();
                    return securityGroup.GetMetaData().AsQueryable();
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

        [WebGet]
        public IQueryable<MenuItem> RefreshMenuItem(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            var context = new MenuSecurityEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.MenuItems
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<MenuItemType> RefreshMenuItemType(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            var context = new MenuSecurityEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.MenuItemTypes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [WebGet]
        public IQueryable<MenuItemCode> RefreshMenuItemCode(string autoIDs)
        {
            var query = from val in autoIDs.Split(',')
                        select long.Parse(val);
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            var context = new MenuSecurityEntities(dalUtility.EntityConectionString);

            var queryResult = (from q in context.MenuItemCodes
                               where query.Contains(q.AutoID)
                               select q);

            return queryResult;
        }

        [ChangeInterceptor("MenuItemTypes")]
        public void OnChangeMenuItemTypes(MenuItemType menuItemType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
                var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
                context.MenuItems.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = menuItemType.CompanyID;
                string typeID = menuItemType.MenuItemTypeID;
                string sqlstring = "UPDATE MenuItems SET MenuItemTypeID = null WHERE CompanyID = '" + companyID + "' and MenuItemTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("MenuItemCodes")]
        public void OnChangeMenuItemTypes(MenuItemCode menuItemCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
                var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
                context.MenuItems.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = menuItemCode.CompanyID;
                string codeID = menuItemCode.MenuItemCodeID;
                string sqlstring = "UPDATE MenuItems SET MenuItemCodeID = null WHERE CompanyID = '" + companyID + "' and MenuItemCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("ExecutableProgramTypes")]
        public void OnChangeExecutableProgramTypes(ExecutableProgramType executableProgramType, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Type was used by its parent record...
                XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
                var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
                context.ExecutablePrograms.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = executableProgramType.CompanyID;
                string typeID = executableProgramType.ExecutableProgramTypeID;
                string sqlstring = "UPDATE ExecutablePrograms SET ExecutableProgramTypeID = null WHERE CompanyID = '" + companyID + "' and ExecutableProgramTypeID = '" + typeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("ExecutableProgramCodes")]
        public void OnChangeExecutableProgramTypes(ExecutableProgramCode executableProgramCode, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the Code was used by its parent record...
                XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
                var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
                context.ExecutablePrograms.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = executableProgramCode.CompanyID;
                string codeID = executableProgramCode.ExecutableProgramCodeID;
                string sqlstring = "UPDATE ExecutablePrograms SET ExecutableProgramCodeID = null WHERE CompanyID = '" + companyID + "' and ExecutableProgramCodeID = '" + codeID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("ExecutablePrograms")]
        public void OnChangeExecutablePrograms(ExecutableProgram executableProgram, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//update a null to any place the item was used by its parent record...
                XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
                var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
                context.ExecutablePrograms.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = executableProgram.CompanyID;
                string executableProgramID = executableProgram.ExecutableProgramID;
                string sqlstring = "UPDATE MenuItems SET ExecutableProgramID = null WHERE CompanyID = '" + companyID + "' and ExecutableProgramID = '" + executableProgramID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [ChangeInterceptor("MenuItems")]
        public void OnChangeMenuItemTypes(MenuItem menuItem, UpdateOperations operations)
        {
            if (operations == UpdateOperations.Delete)
            {//Cascade delete any children belonging to parent Menu Item deleted...
                XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
                var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
                context.MenuItems.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                string companyID = menuItem.CompanyID;
                string menuItemID = menuItem.MenuItemID;
                string sqlstring = "Delete MenuItems WHERE CompanyID = '" + companyID + "' and ParentMenuID = '" + menuItemID + "'";
                context.ExecuteStoreCommand(sqlstring);
            }
        }

        [WebGet]
        public IQueryable<MenuItem> GetMenuItemsAllowedByUser(string systemUserID, string companyID)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
            
            var query = (from sus in context.SystemUserSecurities
                          from ms in context.MenuSecurities
                          from mi in context.MenuItems
                          where sus.CompanyID == companyID && 
                          ms.CompanyID == companyID && 
                          mi.CompanyID == companyID &&
                          sus.SystemUserID == systemUserID &&
                          sus.SecurityGroupID == ms.SecurityGroupID &&
                          ms.MenuItemID == mi.MenuItemID &&
                          mi.AllowAll == false 
                          select mi);


            var query2 = (from mi in context.MenuItems
                          where mi.AllowAll == true && 
                          mi.CompanyID == companyID
                          select mi);
            var mergedList = query.Union(query2);
            return mergedList;
        }

        protected override MenuSecurityEntities CreateDataSource()
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            var context = new MenuSecurityEntities(dalUtility.EntityConectionString);

            //test it
            //GetMenuItemsAllowedByUser("Base");
            //test it...
            //IQueryable<MenuSecurity> query = (from q in _context.MenuSecurities
            //                                  select q);
            //var query = (from mi in _context.MenuItems
            //             from ms in _context.MenuSecurities
            //             where mi.MenuItemID == ms.MenuItemID &&
            //                   mi.AllowAll == true
            //             select mi).ToList();
            //foreach (MenuItem mi in query)
            //{
            //    string s = mi.MenuItemID.ToString();
            //}

            //ToDo: ADD DAL Securities Logic...
            //DAL Security Should require USERID and DALName
            //From those two bits of information we can see if the User has rights to the DAL...
            //By default the DAL will be wide open...  The DalItems and DalSecurities will have
            //to be appended if security at the DAL level is required...
            return context;
        }

    }
}
