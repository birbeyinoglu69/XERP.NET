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
using System.Configuration;
using System.Data.EntityClient;
//All custom code should be placed in this partial class
namespace XERP.Web.Services.MenuItemSecurityGroup
{
    public partial class MenuItemSecurityGroupDomainService
    {
        #region Global Extended Partial Class Functionality
        //Do not modify this method it is required...
        //Use shared global SQL Connection string
        protected override MenuItemSecurityGroupEntities CreateObjectContext()
        {
            string baseSQLConnectionString = ConfigurationManager.ConnectionStrings["BaseSQL"].ConnectionString;
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MenuItemSecurityGroupEntities"].ToString());
            entityConectionString.ProviderConnectionString = baseSQLConnectionString;
            //return base.CreateObjectContext();
            return new MenuItemSecurityGroupEntities(entityConectionString.ConnectionString);
        }
        #endregion
        #region Private Propeties
           
        #endregion

        #region Public Methods
        public IQueryable<MenuItem> GetFlatMenuItems()
        {
            return this.ObjectContext.MenuItems.OrderBy(pi => pi.ParentMenuID);
        }

        public IQueryable<MenuItem> GetByAutoID(Int64 autoID)
        {
            return this.ObjectContext.MenuItems.Where(mi => mi.AutoID == autoID);  
        }

        #endregion       
    }
}