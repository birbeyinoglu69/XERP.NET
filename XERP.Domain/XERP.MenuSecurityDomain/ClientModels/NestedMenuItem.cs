using System;
using System.Collections.ObjectModel;
using XERP.MenuSecurityDomain.MenuSecurityDataService;
using System.Drawing;
using XERP.MenuSecurityDomain.Services;
using System.Windows.Media.Imaging;
namespace XERP.MenuSecurityDomain.ClientModels
{
    public class NestedMenuItem
    {
        private IMenuSecurityServiceAgent _serviceAgent;
        
        public string CompanyID { get; set; }
        public string MenuItemID { get; set; }
        public string ParentMenuID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public string MenuItemTypeID { get; set; }
        public string MenuItemCodeID { get; set; }
        public string ImageID { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? Executable { get; set; }
        public string ExecutablePath { get; set; }
        public string ExecutableProgram { get; set; }
        public bool? HideMenu { get; set; }
        public bool? AllowAll { get; set; }
        public BitmapImage DBImageAsImage { get; set; }
        public Int64 AutoID { get; set; }

        public ObservableCollection<NestedMenuItem> Children { get; set; }

        public NestedMenuItem(MenuItem menuItem)
        {
            CompanyID = menuItem.CompanyID;
            MenuItemID = menuItem.MenuItemID;
            ParentMenuID = menuItem.ParentMenuID;
            Name = menuItem.Name;
            Description = menuItem.Description;
            Active = menuItem.Active;
            MenuItemTypeID = menuItem.MenuItemTypeID;
            MenuItemCodeID = menuItem.MenuItemCodeID;
            ImageID = menuItem.ImageID;
            DisplayOrder = menuItem.DisplayOrder;
            Executable = menuItem.Executable;
            ExecutablePath = menuItem.ExecutablePath;
            ExecutableProgram = menuItem.ExecutableProgram;
            HideMenu = menuItem.HideMenu;
            AllowAll = menuItem.AllowAll;
            AutoID = menuItem.AutoID;
            DBImageAsImage = GetMenuItemImage(menuItem.ImageID, CompanyID);
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nestedMenuItem)
        {
            CompanyID = nestedMenuItem.CompanyID;
            MenuItemID = nestedMenuItem.MenuItemID;
            ParentMenuID = nestedMenuItem.ParentMenuID;
            Name = nestedMenuItem.Name;
            Description = nestedMenuItem.Description;
            Active = nestedMenuItem.Active;
            MenuItemTypeID = nestedMenuItem.MenuItemTypeID;
            MenuItemCodeID = nestedMenuItem.MenuItemCodeID;
            ImageID = nestedMenuItem.ImageID;
            DisplayOrder = nestedMenuItem.DisplayOrder;
            Executable = nestedMenuItem.Executable;
            ExecutablePath = nestedMenuItem.ExecutablePath;
            ExecutableProgram = nestedMenuItem.ExecutableProgram;
            HideMenu = nestedMenuItem.HideMenu;
            AllowAll = nestedMenuItem.AllowAll;
            AutoID = nestedMenuItem.AutoID;
            DBImageAsImage = GetMenuItemImage(nestedMenuItem.ImageID, CompanyID);
            Children = new ObservableCollection<NestedMenuItem>();
        }
        public NestedMenuItem(NestedMenuItem nestedMenuItem, params NestedMenuItem[] children)
        {
            CompanyID = nestedMenuItem.CompanyID;
            MenuItemID = nestedMenuItem.MenuItemID;
            ParentMenuID = nestedMenuItem.ParentMenuID;
            Name = nestedMenuItem.Name;
            Description = nestedMenuItem.Description;
            Active = nestedMenuItem.Active;
            MenuItemTypeID = nestedMenuItem.MenuItemTypeID;
            MenuItemCodeID = nestedMenuItem.MenuItemCodeID;
            ImageID = nestedMenuItem.ImageID;
            DisplayOrder = nestedMenuItem.DisplayOrder;
            Executable = nestedMenuItem.Executable;
            ExecutablePath = nestedMenuItem.ExecutablePath;
            ExecutableProgram = nestedMenuItem.ExecutableProgram;
            HideMenu = nestedMenuItem.HideMenu;
            AllowAll = nestedMenuItem.AllowAll;
            AutoID = nestedMenuItem.AutoID;
            DBImageAsImage = GetMenuItemImage(nestedMenuItem.ImageID, CompanyID);
            ObservableCollection<NestedMenuItem> oc = new ObservableCollection<NestedMenuItem>();
            foreach (var child in children)
                oc.Add(child);
            Children = oc;
        }

        private BitmapImage GetMenuItemImage(string imageID, string companyID)
        {
            _serviceAgent = new MenuSecurityServiceAgent();
            return _serviceAgent.GetMenuItemImage(imageID, companyID);
        }
    }      
}
