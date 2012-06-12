using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using System.IO;

namespace XERP.Client
{
    public class XERPClientConfig
    {
        private XDocument doc;

        public XERPClientConfig()
        {
            //setXERPConfigPath();
            try
            {
                doc = XDocument.Load(XERPConfigPath);
            }
            catch
            {
                try
                {//mmmv pattern can be finicky with its injection and when doing so it was hitting a different pathing...
                    //so if it fails the this catch will use the resource pathing instead....
                    doc =XDocument.Load( XERP.Client.Properties.Resources.DefaultXERPClientConfigPath.ToString());
                }
                 catch(FileNotFoundException ex)
                {
                    throw (ex);
                }
                
            }
        }

        //private string _xERPConfigPath;
        public string XERPConfigPath//get only it is set in the constructor...
        {
            get { return System.Windows.Forms.Application.StartupPath + "\\XERPClientConfig.xml"; }
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
