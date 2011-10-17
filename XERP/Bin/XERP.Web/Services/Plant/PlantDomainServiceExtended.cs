using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.ServiceModel.DomainServices.EntityFramework;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using XERP.Web.Models.Plant;
using System.Configuration;
using System.Data.EntityClient;
//All custom code should be placed in this partial class
namespace XERP.Web.Services.Plant
{
    public partial class PlantDomainService
    {//Do not modify this method it is required...
        //Use shared global SQL Connection string
        protected override PlantEntities CreateObjectContext()
        {
            string baseSQLConnectionString = ConfigurationManager.ConnectionStrings["BaseSQL"].ConnectionString;
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["PlantEntities"].ToString());
            entityConectionString.ProviderConnectionString = baseSQLConnectionString;
            //return base.CreateObjectContext();
            return new PlantEntities(entityConectionString.ConnectionString);
        }
    }
}