//If Schema changes are required all the canned code below will be lost...
//Do not modify this Class use the partial extended class
//If one of the canned CRUD Methods below needs modification
//Cut and paste it to the extended Partial Class and modify their
namespace XERP.Web.Services.Company
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
    using XERP.Web.Models.Company;
    using System.Configuration;
    using System.Data.EntityClient;

    // Implements application logic using the CompanyEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class CompanyDomainService : LinqToEntitiesDomainService<CompanyEntities>
    {
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Companies' query.
        public IQueryable<Company> GetCompanies()
        {   
            return this.ObjectContext.Companies;
        }

        public void InsertCompany(Company company)
        {
            if ((company.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(company, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Companies.AddObject(company);
            }
        }

        public void UpdateCompany(Company currentCompany)
        {
            this.ObjectContext.Companies.AttachAsModified(currentCompany, this.ChangeSet.GetOriginal(currentCompany));
        }

        public void DeleteCompany(Company company)
        {
            if ((company.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(company, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Companies.Attach(company);
                this.ObjectContext.Companies.DeleteObject(company);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CompanyCodes' query.
        public IQueryable<CompanyCode> GetCompanyCodes()
        {
            return this.ObjectContext.CompanyCodes;
        }

        public void InsertCompanyCode(CompanyCode companyCode)
        {
            if ((companyCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(companyCode, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CompanyCodes.AddObject(companyCode);
            }
        }

        public void UpdateCompanyCode(CompanyCode currentCompanyCode)
        {
            this.ObjectContext.CompanyCodes.AttachAsModified(currentCompanyCode, this.ChangeSet.GetOriginal(currentCompanyCode));
        }

        public void DeleteCompanyCode(CompanyCode companyCode)
        {
            if ((companyCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(companyCode, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CompanyCodes.Attach(companyCode);
                this.ObjectContext.CompanyCodes.DeleteObject(companyCode);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CompanyTypes' query.
        public IQueryable<CompanyType> GetCompanyTypes()
        {
            return this.ObjectContext.CompanyTypes;
        }

        public void InsertCompanyType(CompanyType companyType)
        {
            if ((companyType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(companyType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CompanyTypes.AddObject(companyType);
            }
        }

        public void UpdateCompanyType(CompanyType currentCompanyType)
        {
            this.ObjectContext.CompanyTypes.AttachAsModified(currentCompanyType, this.ChangeSet.GetOriginal(currentCompanyType));
        }

        public void DeleteCompanyType(CompanyType companyType)
        {
            if ((companyType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(companyType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CompanyTypes.Attach(companyType);
                this.ObjectContext.CompanyTypes.DeleteObject(companyType);
            }
        }
    }
}


