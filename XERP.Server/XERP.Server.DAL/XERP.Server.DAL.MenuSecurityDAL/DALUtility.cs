using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace XERP.Server.DAL.MenuSecurityDAL
{
    public class DALUtility
    {
        public const string _metadataString = @"res://*/MenuSecurity.csdl|res://*/MenuSecurity.ssdl|res://*/MenuSecurity.msl";

        private EntityConnectionStringBuilder _entityBuilder = new EntityConnectionStringBuilder();
        public EntityConnectionStringBuilder EntityBuilder
        {
            get 
            {
                XERP.Server.DAL.DALConfig dalUtility = new XERP.Server.DAL.DALConfig();
                _entityBuilder.Provider = dalUtility.ProviderName;
                _entityBuilder.ProviderConnectionString = dalUtility.BaseSQLConnectionString;
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
