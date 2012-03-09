using System;
namespace XERP.MenuSecurityDomain.Services
{
    public interface IMenuSecurityServiceAgent
    {
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemByID(string menuItemID);
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemChildren(string menuItemID);
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemsAvailableToUser(string systemUserID);
    }
}
