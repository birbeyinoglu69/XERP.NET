using System;
using System.Collections.ObjectModel;
using XERP.MenuSecurityDomain.MenuSecurityDataService;
namespace XERP.MenuSecurityDomain.ClientModels
{
    public class NestedMenuItem
    {
        public string CompanyID { get; set; }
        public string MenuItemID { get; set; }
        public string ParentMenuID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Int64 AutoID { get; set; }
        public ObservableCollection<NestedMenuItem> Children { get; set; }

        //public NestedMenuItem(string companyID, string menuItemID, string parentMenuID, Int64 autoID)
        //{
        //    CompanyID = companyID;
        //    MenuItemID = menuItemID;
        //    ParentMenuID = parentMenuID;
        //    AutoID = autoID;
        //    Children = new ObservableCollection<NestedMenuItem>();
        //}
        public NestedMenuItem(MenuItem menuItem)
        {
            CompanyID = menuItem.CompanyID;
            MenuItemID = menuItem.MenuItemID;
            ParentMenuID = menuItem.ParentMenuID;
            Name = menuItem.Name;
            Description = menuItem.Description;
            AutoID = menuItem.AutoID;
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nestedMenuItem)
        {
            CompanyID = nestedMenuItem.CompanyID;
            MenuItemID = nestedMenuItem.MenuItemID;
            ParentMenuID = nestedMenuItem.ParentMenuID;
            Name = nestedMenuItem.Name;
            Description = nestedMenuItem.Description;
            AutoID = nestedMenuItem.AutoID;
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nestedMenuItem, params NestedMenuItem[] children)
        {
            CompanyID = nestedMenuItem.CompanyID;
            MenuItemID = nestedMenuItem.MenuItemID;
            ParentMenuID = nestedMenuItem.ParentMenuID;
            Description = nestedMenuItem.Description;
            AutoID = nestedMenuItem.AutoID;
            AutoID = nestedMenuItem.AutoID;
            ObservableCollection<NestedMenuItem> oc = new ObservableCollection<NestedMenuItem>();
            foreach (var child in children)
                oc.Add(child);
            Children = oc;
        }

    }      
}
