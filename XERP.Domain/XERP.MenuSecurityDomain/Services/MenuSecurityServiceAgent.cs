using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.MenuSecurityDomain.MenuSecurityDataService;
using System.Data.Services.Client;
using System.Reflection;
using System.Collections.ObjectModel;
using XERP.MenuSecurityDomain.Services;
using XERP.Client;
using System.Linq.Dynamic;


namespace XERP.MenuSecurityDomain.Services
{
    public class MenuSecurityServiceAgent : XERP.MenuSecurityDomain.Services.IMenuSecurityServiceAgent
    {
        private ServiceUtility _serviceUtility = new ServiceUtility();
        private Uri _rootUri;
        private MenuSecurityEntities _context;

        public MenuSecurityServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = _serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new MenuSecurityEntities(_rootUri); 
        }

        public IEnumerable<MenuItem> GetMenuItemsAvailableToUser(string systemUserID)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<MenuItem>("GetMenuItemsAllowedByUser").AddQueryOption("systemUserID", "'Base'");
            return query;
        }

        public IEnumerable<MenuItem> GetMenuItemByID(string menuItemID)
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var queryRelult = (from q in _context.MenuItems
                               where q.MenuItemID == menuItemID
                               select q).ToList();
            return queryRelult;
        }

        public IEnumerable<MenuItem> GetMenuItemChildren(string menuItemID)
        {
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var queryRelult = (from q in _context.MenuItems
                               where q.ParentMenuID == menuItemID
                               select q);
            return queryRelult;
        }
    }
}
