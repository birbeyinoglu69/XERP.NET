
namespace XERP.Web.Services.MenuItemSecurityGroup
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using XERP.Web.Models.MenuItemSecurityGroup;


    // Implements application logic using the MenuItemSecurityGroupEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class MenuItemSecurityGroupDomainService : LinqToEntitiesDomainService<MenuItemSecurityGroupEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Companies' query.
        public IQueryable<Company> GetCompanies()
        {
            return this.ObjectContext.Companies;
        }
        //Modified with Order By and Moved to Extended.cs
        //// TODO:  
        //// Consider constraining the results of your query method.  If you need additional input you can
        //// add parameters to this method or create additional query methods with different names.
        //// To support paging you will need to add ordering to the 'MenuItems' query.
        public IQueryable<MenuItem> GetMenuItems()
        {
            return this.ObjectContext.MenuItems;
        }

        public void InsertMenuItem(MenuItem menuItem)
        {
            if ((menuItem.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuItem, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MenuItems.AddObject(menuItem);
            }
        }

        public void UpdateMenuItem(MenuItem currentMenuItem)
        {
            this.ObjectContext.MenuItems.AttachAsModified(currentMenuItem, this.ChangeSet.GetOriginal(currentMenuItem));
        }

        public void DeleteMenuItem(MenuItem menuItem)
        {
            if ((menuItem.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuItem, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MenuItems.Attach(menuItem);
                this.ObjectContext.MenuItems.DeleteObject(menuItem);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MenuItemCodes' query.
        public IQueryable<MenuItemCode> GetMenuItemCodes()
        {
            return this.ObjectContext.MenuItemCodes;
        }

        public void InsertMenuItemCode(MenuItemCode menuItemCode)
        {
            if ((menuItemCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuItemCode, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MenuItemCodes.AddObject(menuItemCode);
            }
        }

        public void UpdateMenuItemCode(MenuItemCode currentMenuItemCode)
        {
            this.ObjectContext.MenuItemCodes.AttachAsModified(currentMenuItemCode, this.ChangeSet.GetOriginal(currentMenuItemCode));
        }

        public void DeleteMenuItemCode(MenuItemCode menuItemCode)
        {
            if ((menuItemCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuItemCode, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MenuItemCodes.Attach(menuItemCode);
                this.ObjectContext.MenuItemCodes.DeleteObject(menuItemCode);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MenuItemTypes' query.
        public IQueryable<MenuItemType> GetMenuItemTypes()
        {
            return this.ObjectContext.MenuItemTypes;
        }

        public void InsertMenuItemType(MenuItemType menuItemType)
        {
            if ((menuItemType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuItemType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MenuItemTypes.AddObject(menuItemType);
            }
        }

        public void UpdateMenuItemType(MenuItemType currentMenuItemType)
        {
            this.ObjectContext.MenuItemTypes.AttachAsModified(currentMenuItemType, this.ChangeSet.GetOriginal(currentMenuItemType));
        }

        public void DeleteMenuItemType(MenuItemType menuItemType)
        {
            if ((menuItemType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuItemType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MenuItemTypes.Attach(menuItemType);
                this.ObjectContext.MenuItemTypes.DeleteObject(menuItemType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'MenuSecurities' query.
        public IQueryable<MenuSecurity> GetMenuSecurities()
        {
            return this.ObjectContext.MenuSecurities;
        }

        public void InsertMenuSecurity(MenuSecurity menuSecurity)
        {
            if ((menuSecurity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuSecurity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MenuSecurities.AddObject(menuSecurity);
            }
        }

        public void UpdateMenuSecurity(MenuSecurity currentMenuSecurity)
        {
            this.ObjectContext.MenuSecurities.AttachAsModified(currentMenuSecurity, this.ChangeSet.GetOriginal(currentMenuSecurity));
        }

        public void DeleteMenuSecurity(MenuSecurity menuSecurity)
        {
            if ((menuSecurity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(menuSecurity, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MenuSecurities.Attach(menuSecurity);
                this.ObjectContext.MenuSecurities.DeleteObject(menuSecurity);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SecurityGroups' query.
        public IQueryable<SecurityGroup> GetSecurityGroups()
        {
            return this.ObjectContext.SecurityGroups;
        }

        public void InsertSecurityGroup(SecurityGroup securityGroup)
        {
            if ((securityGroup.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityGroup, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SecurityGroups.AddObject(securityGroup);
            }
        }

        public void UpdateSecurityGroup(SecurityGroup currentSecurityGroup)
        {
            this.ObjectContext.SecurityGroups.AttachAsModified(currentSecurityGroup, this.ChangeSet.GetOriginal(currentSecurityGroup));
        }

        public void DeleteSecurityGroup(SecurityGroup securityGroup)
        {
            if ((securityGroup.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityGroup, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SecurityGroups.Attach(securityGroup);
                this.ObjectContext.SecurityGroups.DeleteObject(securityGroup);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SecurityGroupCodes' query.
        public IQueryable<SecurityGroupCode> GetSecurityGroupCodes()
        {
            return this.ObjectContext.SecurityGroupCodes;
        }

        public void InsertSecurityGroupCode(SecurityGroupCode securityGroupCode)
        {
            if ((securityGroupCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityGroupCode, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SecurityGroupCodes.AddObject(securityGroupCode);
            }
        }

        public void UpdateSecurityGroupCode(SecurityGroupCode currentSecurityGroupCode)
        {
            this.ObjectContext.SecurityGroupCodes.AttachAsModified(currentSecurityGroupCode, this.ChangeSet.GetOriginal(currentSecurityGroupCode));
        }

        public void DeleteSecurityGroupCode(SecurityGroupCode securityGroupCode)
        {
            if ((securityGroupCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityGroupCode, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SecurityGroupCodes.Attach(securityGroupCode);
                this.ObjectContext.SecurityGroupCodes.DeleteObject(securityGroupCode);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SecurityGroupTypes' query.
        public IQueryable<SecurityGroupType> GetSecurityGroupTypes()
        {
            return this.ObjectContext.SecurityGroupTypes;
        }

        public void InsertSecurityGroupType(SecurityGroupType securityGroupType)
        {
            if ((securityGroupType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityGroupType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SecurityGroupTypes.AddObject(securityGroupType);
            }
        }

        public void UpdateSecurityGroupType(SecurityGroupType currentSecurityGroupType)
        {
            this.ObjectContext.SecurityGroupTypes.AttachAsModified(currentSecurityGroupType, this.ChangeSet.GetOriginal(currentSecurityGroupType));
        }

        public void DeleteSecurityGroupType(SecurityGroupType securityGroupType)
        {
            if ((securityGroupType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(securityGroupType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SecurityGroupTypes.Attach(securityGroupType);
                this.ObjectContext.SecurityGroupTypes.DeleteObject(securityGroupType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemUsers' query.
        public IQueryable<SystemUser> GetSystemUsers()
        {
            return this.ObjectContext.SystemUsers;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemUserSecurities' query.
        public IQueryable<SystemUserSecurity> GetSystemUserSecurities()
        {
            return this.ObjectContext.SystemUserSecurities;
        }
    }
}


