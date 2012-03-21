using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Configuration;
using System.Data.EntityClient;
using XERP.Server;
using XERP.Server.DAL.MenuSecurityDAL;

namespace XERP.Server.Service.MenuSecurityService
{
    public class MenuSecurityService : DataService< MenuSecurityEntities >
    {
        private MenuSecurityEntities _context;
        
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetEntitySetPageSize("*", 50);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }

        [WebGet]
        public IQueryable<MenuItem> GetMenuItemsAllowedByUser(string systemUserID)
        {//complex query required compound search criteria so this had to be done server side...  
            var query = (from mi in _context.MenuItems
                         from ms in _context.MenuSecurities
                         where mi.MenuItemID == ms.MenuItemID &&
                               mi.AllowAll == false
                         select mi);

            var query2 = (from mi in _context.MenuItems
                          where mi.AllowAll == true
                          select mi);
            var mergedList = query.Union(query2); 
            return mergedList;
        }

        protected override MenuSecurityEntities CreateDataSource()
        {
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MenuSecurityEntities"].ToString());

            XERPServerConfig config = new XERPServerConfig();
            entityConectionString.ProviderConnectionString = config.BaseSQLConnectionString;
            _context = new MenuSecurityEntities(entityConectionString.ConnectionString);

            //test it...
            //IQueryable<MenuSecurity> query = (from q in _context.MenuSecurities
            //                                select q);
            //var query = (from mi in _context.MenuItems
            //             from ms in _context.MenuSecurities
            //             where mi.MenuItemID == ms.MenuItemID &&
            //                   mi.AllowAll == false
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
            
            return _context;
        }
    }
}
