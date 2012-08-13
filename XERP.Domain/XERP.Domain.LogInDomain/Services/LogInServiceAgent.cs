using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using System.Reflection;
using System.Collections.ObjectModel;
using XERP.Client;
using XERP.Domain.LogInDomain.LogInDataService;

namespace XERP.Domain.LogInDomain.Services
{
    public class LogInServiceAgent : XERP.Domain.LogInDomain.Services.ILogInServiceAgent
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
            var authenticatedQueryResult = (from q in _context.SystemUsers.Expand("SystemUserSecurities")
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

        private IEnumerable<ExecutableProgram> GetExecutableProgramsAllowedByUser(string systemUserID)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<ExecutableProgram>("GetExecutableProgramsAllowedByUser").AddQueryOption("SystemUserID", "'" + systemUserID + "'");
            return query;
        }

        //return executableprogramIDs as string list...
        //this is fed to the session to cache executable programs allowed to the user...
        private List<string> GetExecutableProgramIDsAllowedByUser(string systemUserID)
        {
            List<string> rList = new List<string>();
            List<ExecutableProgram> executablePrograms = GetExecutableProgramsAllowedByUser(systemUserID).ToList();
            foreach (ExecutableProgram executableProgram in executablePrograms)
            {
                rList.Add(executableProgram.ExecutableProgramID);
            }
            return rList;
        }

        private void SetSessionSystemUser(SystemUser systemUser)
        {   //cache Current CompanyID
            ClientSessionSingleton.Instance.CompanyID = systemUser.DefaultCompanyID;
            //cache User
            ClientSessionSingleton.Instance.SystemUserID = systemUser.SystemUserID;
            //cache groups user belongs to...
            ClientSessionSingleton.Instance.SessionIsAuthentic = true;
            foreach(SystemUserSecurity systemUserSecurity in systemUser.SystemUserSecurities)
            {
                ClientSessionSingleton.Instance.SecurityGroupIDList.Add(systemUserSecurity.SecurityGroupID.ToString());
            }
            //cache Executable Programs available to the User...
            ClientSessionSingleton.Instance.ExecutableProgramIDList = GetExecutableProgramIDsAllowedByUser(systemUser.SystemUserID);
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }
    }
}
