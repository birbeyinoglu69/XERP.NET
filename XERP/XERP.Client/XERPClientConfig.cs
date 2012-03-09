using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace XERP.Client
{
    public class XERPClientConfig
    {
        private XDocument doc;

        public XERPClientConfig()
        {
            setXERPConfigPath();
            doc = XDocument.Load(XERPConfigPath);
        }

        private string _xERPConfigPath;
        public string XERPConfigPath//get only it is set in the constructor...
        {
            get { return _xERPConfigPath; }
        }
        //private only no public setter as it will be set in the XERP.Resources or the WEB Config AppSetting
        private void setXERPConfigPath()
        {
            //Get Default XERPConfigPath it is a resource string associated to the main XERP.dll assembly...
            _xERPConfigPath = XERP.Client.Properties.Resources.DefaultXERPClientConfigPath.ToString();
            //get XERPConfig Path from WEBConfig or APPConfig when available this will overide the defaulted value
            //Allowing it to be changed on deployment...
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            if (appSettings.Count > 0)
            {
                for (int i = 0; i < appSettings.Count; i++)
                {
                    if (appSettings.GetKey(i).ToString().ToLower() == "xerpconfigpath")
                    {
                        _xERPConfigPath = appSettings[i].ToString();
                        return;
                    }
                }
            }
        }

        private string configURI;
        public string ConfigURI
        {
            get
            {
                var e = doc.Element("Config").Elements("URIS").
                   Elements("URI").Single(x => (string)x.Attribute("ID") == "BaseURI");
                configURI = e.Attribute("Value").Value;
                return configURI;
            }
            set
            {
                configURI = value;
                var e = doc.Element("Config").Elements("URIS").
                    Elements("URI").Single(x => (string)x.Attribute("ID") == "BaseURI");
                e.Attribute("Value").Value = configURI;
                using (var writer = new XmlTextWriter(XERPConfigPath, new UTF8Encoding(false)))
                {
                    doc.Save(writer);
                }
            }
        }

        public Token GetBaseToken()
        {
            var e = doc.Element("Config").Elements("Tokens").
                Elements("Token").Single(x => (string)x.Attribute("ID") == "BaseToken");
            Token rToken = new Token(e.Attribute("ID").Value, e.Attribute("Value").Value, e.Attribute("Required").Value);

            return rToken;
        }

        public void SaveBaseToken(Token _token)
        {
            var e = doc.Element("Config").Elements("Tokens").
                Elements("Token").Single(x => (string)x.Attribute("ID") == "BaseToken");
            e.Attribute("Value").Value = _token.TokenValue;
            e.Attribute("Required").Value = _token.Required;
            using (var writer = new XmlTextWriter(XERPConfigPath, new UTF8Encoding(false)))
            {
                doc.Save(writer);
            }
        }
    }

    public class Token
    {
        //private XDocument doc;
        public Token(string _iD, string _tokenValue, string _required)
        {
            iD = _iD;
            tokenValue = _tokenValue;
            required = _required;
        }

        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string tokenValue;
        public string TokenValue
        {
            get { return tokenValue; }
            set { tokenValue = value; }
        }

        private string required;
        public string Required
        {
            get { return required; }
            set { required = value; }
        }
    }
}
