using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace XERP.Server.DAL.LogInDAL
{
    public class DALUtility
    {
        public const string _metadataString = @"res://*/LogIn.csdl|res://*/LogIn.ssdl|res://*/LogIn.msl";

        private EntityConnectionStringBuilder _entityBuilder = new EntityConnectionStringBuilder();
        public EntityConnectionStringBuilder EntityBuilder
        {
            get 
            {
                XERP.Server.DAL.DALConfig dalConfig = new XERP.Server.DAL.DALConfig();
                _entityBuilder.Provider = dalConfig.ProviderName;
                _entityBuilder.ProviderConnectionString = dalConfig.BaseSQLConnectionString;
                _entityBuilder.Metadata = _metadataString;
                return _entityBuilder; 
            }
        }

        public string EntityConectionString
        {
            get { return EntityBuilder.ConnectionString; }
        }

        public DALUtility()
        {
            
          
        }
    }
}
