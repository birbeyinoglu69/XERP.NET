using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using XERP.Server.DAL.CompanyDAL;
using System.Data.EntityClient;
using XERP.Server;
using System.Configuration;
namespace XERP.Server.Service.CompanyService
{
    public class CompanyDataService : DataService< CompanyEntities >
    {
        private CompanyEntities _context;
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetEntitySetPageSize("*", 50);
        }

        protected override CompanyEntities CreateDataSource()
        {
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["CompanyEntities"].ToString());

            XERPServerConfig config = new XERPServerConfig();
            entityConectionString.ProviderConnectionString = config.BaseSQLConnectionString;
            var _context = new CompanyEntities(entityConectionString.ConnectionString);

            //test it...
            //IQueryable<Company> CompanyQuery = (from c in _context.Companies
            //                                    select c);

            //foreach (Company cc in CompanyQuery)
            //{
            //    string s = cc.Name.ToString();
            //}

            //ToDo: ADD DAL Securities Logic...
                //DAL Security Should require USERID and DALName
                //From those two bits of information we can see if the User has rights to the DAL...
                //By default the DAL will be wide open...  The DalItems and DalSecurities will have
                //to be appended if security at the DAL level is required...

            return _context;
        }
    }
}
