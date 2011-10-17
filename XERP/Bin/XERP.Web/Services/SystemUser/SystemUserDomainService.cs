
namespace XERP.Web.Services.SystemUser
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
    using XERP.Web.Models.SystemUser;


    // Implements application logic using the SystemUserEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class SystemUserDomainService : LinqToEntitiesDomainService<SystemUserEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Addresses' query.
        public IQueryable<Address> GetAddresses()
        {
            return this.ObjectContext.Addresses;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Companies' query.
        public IQueryable<Company> GetCompanies()
        {
            return this.ObjectContext.Companies;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Employees' query.
        public IQueryable<Employee> GetEmployees()
        {
            return this.ObjectContext.Employees;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SecurityGroups' query.
        public IQueryable<SecurityGroup> GetSecurityGroups()
        {
            return this.ObjectContext.SecurityGroups;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemUsers' query.
        public IQueryable<SystemUser> GetSystemUsers()
        {
            return this.ObjectContext.SystemUsers;
        }

        public void InsertSystemUser(SystemUser systemUser)
        {
            if ((systemUser.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUser, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SystemUsers.AddObject(systemUser);
            }
        }

        public void UpdateSystemUser(SystemUser currentSystemUser)
        {
            this.ObjectContext.SystemUsers.AttachAsModified(currentSystemUser, this.ChangeSet.GetOriginal(currentSystemUser));
        }

        public void DeleteSystemUser(SystemUser systemUser)
        {
            if ((systemUser.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUser, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SystemUsers.Attach(systemUser);
                this.ObjectContext.SystemUsers.DeleteObject(systemUser);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemUserCodes' query.
        public IQueryable<SystemUserCode> GetSystemUserCodes()
        {
            return this.ObjectContext.SystemUserCodes;
        }

        public void InsertSystemUserCode(SystemUserCode systemUserCode)
        {
            if ((systemUserCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUserCode, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SystemUserCodes.AddObject(systemUserCode);
            }
        }

        public void UpdateSystemUserCode(SystemUserCode currentSystemUserCode)
        {
            this.ObjectContext.SystemUserCodes.AttachAsModified(currentSystemUserCode, this.ChangeSet.GetOriginal(currentSystemUserCode));
        }

        public void DeleteSystemUserCode(SystemUserCode systemUserCode)
        {
            if ((systemUserCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUserCode, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SystemUserCodes.Attach(systemUserCode);
                this.ObjectContext.SystemUserCodes.DeleteObject(systemUserCode);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemUserSecurities' query.
        public IQueryable<SystemUserSecurity> GetSystemUserSecurities()
        {
            return this.ObjectContext.SystemUserSecurities;
        }

        public void InsertSystemUserSecurity(SystemUserSecurity systemUserSecurity)
        {
            if ((systemUserSecurity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUserSecurity, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SystemUserSecurities.AddObject(systemUserSecurity);
            }
        }

        public void UpdateSystemUserSecurity(SystemUserSecurity currentSystemUserSecurity)
        {
            this.ObjectContext.SystemUserSecurities.AttachAsModified(currentSystemUserSecurity, this.ChangeSet.GetOriginal(currentSystemUserSecurity));
        }

        public void DeleteSystemUserSecurity(SystemUserSecurity systemUserSecurity)
        {
            if ((systemUserSecurity.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUserSecurity, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SystemUserSecurities.Attach(systemUserSecurity);
                this.ObjectContext.SystemUserSecurities.DeleteObject(systemUserSecurity);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'SystemUserTypes' query.
        public IQueryable<SystemUserType> GetSystemUserTypes()
        {
            return this.ObjectContext.SystemUserTypes;
        }

        public void InsertSystemUserType(SystemUserType systemUserType)
        {
            if ((systemUserType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUserType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SystemUserTypes.AddObject(systemUserType);
            }
        }

        public void UpdateSystemUserType(SystemUserType currentSystemUserType)
        {
            this.ObjectContext.SystemUserTypes.AttachAsModified(currentSystemUserType, this.ChangeSet.GetOriginal(currentSystemUserType));
        }

        public void DeleteSystemUserType(SystemUserType systemUserType)
        {
            if ((systemUserType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(systemUserType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SystemUserTypes.Attach(systemUserType);
                this.ObjectContext.SystemUserTypes.DeleteObject(systemUserType);
            }
        }
    }
}


