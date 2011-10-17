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
//All custom code should be placed in this partial class
namespace XERP.Web.Services.Company
{
    public partial class CompanyDomainService
    {//Do not modify this method it is required...
        //Use shared global SQL Connection string
        protected override CompanyEntities CreateObjectContext()
        {
            string baseSQLConnectionString = ConfigurationManager.ConnectionStrings["BaseSQL"].ConnectionString;
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["CompanyEntities"].ToString());
            entityConectionString.ProviderConnectionString = baseSQLConnectionString;
            //return base.CreateObjectContext();
            return new CompanyEntities(entityConectionString.ConnectionString);
        }
    }
}