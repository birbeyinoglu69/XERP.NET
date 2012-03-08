using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XERP.LogInDomain.LogInDataService;
using System.Data.Services.Client;
using System.Reflection;
using System.Collections.ObjectModel;
using XERP.LogInDomain.Services;
using XERP.Client;

namespace XERP.LogInDomain.Services
{
    public class LogInServiceAgent : XERP.LogInDomain.Services.ILogInServiceAgent
    {
        private ServiceUtility _serviceUtility = new ServiceUtility();
        
        public LogInServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = _serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new LogInEntities(_rootUri); 
        }

        private Uri _rootUri;
        private LogInEntities _context;

        public bool Authenticated(string systemUserID, string password, out string authenticationMessage)
        {
            authenticationMessage = "";
            const string postFixAuthenticationNotAllowedMessage = "Contact the XERP System Administrator for further assistance.";
               
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            //check if system user exists...
            var systemUserQueryRelult = (from q in _context.SystemUsers
                               where q.SystemUserID == systemUserID
                               select q);
            if (systemUserQueryRelult.ToList().Count() == 0)
            {
                authenticationMessage =
                    "User " + systemUserID + " does not exist.  " + postFixAuthenticationNotAllowedMessage;
                return false;
            }

            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            //check if systemuser and password exist...
            var authenticatedQueryResult = (from q in _context.SystemUsers
                               where q.SystemUserID == systemUserID &&
                                     q.Password == password
                               select q);
            if (authenticatedQueryResult.ToList().Count() == 0)
            {
                authenticationMessage =
                    "Password " + password + " is not correct.";
                return false;
            }
            else
            {
                //user is authenticated check if is acount is active...
                //SystemUser authenticatedSystemUser = new SystemUser();
                //authenticatedSystemUser = authenticatedQueryResult.FirstOrDefault();
                if ((bool)authenticatedQueryResult.FirstOrDefault().Active != true)
                {
                    authenticationMessage =
                    "Your XERP Account requires activation.  " + postFixAuthenticationNotAllowedMessage;
                    return false;
                }
                //user is authenticated check if password has expired...
                if ((bool)authenticatedQueryResult.FirstOrDefault().PasswordExpired)
                {
                    authenticationMessage =
                    "Your password has expired.  " + postFixAuthenticationNotAllowedMessage;
                    return false;
                }
            }
            //user is authenticate return true...
            authenticationMessage = "Login Successful";
            SetSessionSystemUser(authenticatedQueryResult.FirstOrDefault());
            return true;
        }

        private void SetSessionSystemUser(SystemUser systemUser)
        {
            ClientSessionSingleton.Instance.CompanyID = systemUser.DefaultCompanyID;
            ClientSessionSingleton.Instance.SystemUserID = systemUser.SystemUserID;
        }
    }
}
