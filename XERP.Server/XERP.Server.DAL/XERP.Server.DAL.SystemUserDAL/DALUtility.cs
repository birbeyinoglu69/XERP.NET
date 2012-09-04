using System.Data.EntityClient;

namespace XERP.Server.DAL.SystemUserDAL
{
    public class DALUtility
    {
        public const string _metadataString = @"res://*/SystemUser.csdl|res://*/SystemUser.ssdl|res://*/SystemUser.msl";

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
