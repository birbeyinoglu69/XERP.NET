using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using XERP.Server.DAL.SystemUserDAL;
using System.Data.EntityClient;
using XERP.Server;
using System.Configuration;
namespace XERP.Server.Service.SystemUserService
{
    public class SystemUserService : DataService< SystemUserEntities >
    {
        private SystemUserEntities _context;
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetEntitySetPageSize("*", 50);
        }

        protected override SystemUserEntities CreateDataSource()
        {
            EntityConnectionStringBuilder entityConectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MenuSecurityEntities"].ToString());

            XERPServerConfig config = new XERPServerConfig();
            entityConectionString.ProviderConnectionString = config.BaseSQLConnectionString;
            _context = new SystemUserEntities(entityConectionString.ConnectionString);

            //test it...
            IQueryable<SystemUser> query = (from q in _context.SystemUsers
                                            select q);
            foreach (SystemUser su in query)
            {
                string s = su.SystemUserID.ToString();
            }

            //ToDo: ADD DAL Securities Logic...
            //DAL Security Should require USERID and DALName
            //From those two bits of information we can see if the User has rights to the DAL...
            //By default the DAL will be wide open...  The DalItems and DalSecurities will have
            //to be appended if security at the DAL level is required...

            return _context;
        }
    }
}
