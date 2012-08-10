using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using XERP.Server.DAL.MenuSecurityDAL;
using System.Data.EntityClient;
using System.Configuration;
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
                case "ExecutableProgramsTypes":
                    ExecutableProgramType executableProgramsType = new ExecutableProgramType();
                    return executableProgramsType.GetMetaData().AsQueryable();
                case "ExecutableProgramsCodes":
                    ExecutableProgramCode executableProgramsCode = new ExecutableProgramCode();
                    return executableProgramsCode.GetMetaData().AsQueryable();
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
        public IQueryable<MenuItem> GetMenuItemsAllowedByUser(string systemUserID)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            var context = new MenuSecurityEntities(dalUtility.EntityConectionString);
            
            var query = (from sus in context.SystemUserSecurities
                          from ms in context.MenuSecurities
                          from mi in context.MenuItems
                          where sus.SystemUserID == systemUserID &&
                          sus.SecurityGroupID == ms.SecurityGroupID &&
                          ms.MenuItemID == mi.MenuItemID &&
                          mi.AllowAll == false
                          select mi);


            var query2 = (from mi in context.MenuItems
                          where mi.AllowAll == true
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
