using System;
using System.Windows.Media.Imaging;
namespace XERP.MenuSecurityDomain.Services
{
    public interface IMenuSecurityServiceAgent
    {
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemByAutoID(long autoID);
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemByID(string menuItemID, string companyID);
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemChildren(string menuItemID, string companyID);
        BitmapImage GetMenuItemImage(string imageID, string companyID);
        System.Collections.Generic.IEnumerable<XERP.MenuSecurityDomain.MenuSecurityDataService.MenuItem> GetMenuItemsAvailableToUser(string systemUserID);
    }
}
