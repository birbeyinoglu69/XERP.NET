
namespace XERP.Server.DAL
{
    public class DALConfig
    {
        private const string _providerName = "System.Data.SQLClient";
        public string ProviderName
        {
            get { return _providerName; }
        }

        public string BaseSQLConnectionString
        {
            get
            {
                XERPServerConfig config = new XERPServerConfig();
                return config.BaseSQLConnectionString;
            }
            set
            {
                XERPServerConfig config = new XERPServerConfig();
                config.BaseSQLConnectionString = value;
            }
        }
        public DALConfig()
        {

        }
    }
}
