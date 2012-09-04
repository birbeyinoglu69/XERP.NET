using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.SystemUserDomain.SystemUserDataService;
using XERP.Domain.SystemUserDomain.Services;

namespace XERP.Domain.SystemUserDomain
{
    public class SystemUserSingletonRepository
    {
        private SystemUserSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SystemUserEntities(_rootUri);
        }
        
        private static SystemUserSingletonRepository _instance;
        public static SystemUserSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SystemUserSingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private SystemUserEntities _repositoryContext;

        public IEnumerable<SystemUser> GetSystemUsers()
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUsers
                             select q);
            return queryResult;
        }

        //public IEnumerable<SystemUserSecurity> GetSystemUserSecurities()
        //{
        //    _repositoryContext = new SystemUserEntities(_rootUri);
        //    _repositoryContext.MergeOption = MergeOption.AppendOnly;
        //    _repositoryContext.IgnoreResourceNotFoundException = true;
        //    var queryResult = (from q in _repositoryContext.SystemUserSecurities
        //                       select q);
        //    return queryResult;
        //}

        public IEnumerable<SystemUser> GetSystemUsers(SystemUser systemUserQuerryObject)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SystemUsers
                             select q;
            
            if  (!string.IsNullOrEmpty(systemUserQuerryObject.Name))
            {
                queryResult = queryResult.Where(q => q.Name.StartsWith(systemUserQuerryObject.Name.ToString())); 
            }

            if (!string.IsNullOrEmpty(systemUserQuerryObject.SystemUserTypeID))
            {
                queryResult = queryResult.Where(q => q.SystemUserTypeID.StartsWith(systemUserQuerryObject.SystemUserTypeID.ToString()));
            }

            if (!string.IsNullOrEmpty(systemUserQuerryObject.SystemUserCodeID))
            {
                queryResult = queryResult.Where(q => q.SystemUserCodeID.StartsWith(systemUserQuerryObject.SystemUserCodeID.ToString()));
            }
            return queryResult;
        }

        //public IEnumerable<SystemUserSecurity> GetSystemUserSecurities(SystemUserSecurity systemUserSecurityQuerryObject)
        //{
        //    _repositoryContext = new SystemUserEntities(_rootUri);
        //    _repositoryContext.MergeOption = MergeOption.AppendOnly;
        //    _repositoryContext.IgnoreResourceNotFoundException = true;
        //    var queryResult = from q in _repositoryContext.SystemUserSecurities
        //                      select q;
        //    if (!string.IsNullOrEmpty(systemUserSecurityQuerryObject.CompanyID))
        //    {
        //        queryResult = queryResult.Where(q => q.CompanyID.StartsWith(systemUserSecurityQuerryObject.CompanyID.ToString()));
        //    }

        //    if (!string.IsNullOrEmpty(systemUserSecurityQuerryObject.SystemUserID))
        //    {
        //        queryResult = queryResult.Where(q => q.SystemUserID.StartsWith(systemUserSecurityQuerryObject.SystemUserID.ToString()));
        //    }

        //    if (!string.IsNullOrEmpty(systemUserSecurityQuerryObject.SecurityGroupID))
        //    {
        //        queryResult = queryResult.Where(q => q.SecurityGroupID.StartsWith(systemUserSecurityQuerryObject.SecurityGroupID.ToString()));
        //    }
        //    return queryResult;
        //}

        public IEnumerable<SystemUser> GetSystemUserByID(string systemUserID)
        {
            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SystemUsers
                          where q.SystemUserID == systemUserID
                          select q);
            
            return queryResult;
        }

        //public IEnumerable<SystemUserSecurity> GetSystemUserSecurityByID(long autoID)
        //{
        //    _repositoryContext = new SystemUserEntities(_rootUri);
        //    _repositoryContext.MergeOption = MergeOption.AppendOnly;
        //    _repositoryContext.IgnoreResourceNotFoundException = true;
        //    var queryResult = (from q in _repositoryContext.SystemUserSecurities
        //                       where q.AutoID == autoID
        //                       select q);

        //    return queryResult;
        //}

        public IEnumerable<SystemUser> Refresh(string autoIDs)
        {

            _repositoryContext = new SystemUserEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SystemUser>("RefreshSystemUser").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SystemUser systemUser)
        {
            if (_repositoryContext.GetEntityDescriptor(systemUser) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(systemUser);
            }
        }

        public void AddToRepository(SystemUser systemUser)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSystemUsers(systemUser);
        }

        public void DeleteFromRepository(SystemUser systemUser)
        {
            if (_repositoryContext.GetEntityDescriptor(systemUser) != null)
            {
                //if it exists in the db delete it from the db
                SystemUserEntities context = new SystemUserEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SystemUser deletedSystemUser = (from q in context.SystemUsers
                                          where q.SystemUserID == systemUser.SystemUserID
                                          select q).SingleOrDefault();
                if (deletedSystemUser != null)
                {
                    context.DeleteObject(deletedSystemUser);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetSystemUserEntityState(systemUser) != EntityStates.Detached)
                {
                    _repositoryContext.Detach(systemUser);
                }
            }
        }

        public EntityStates GetSystemUserEntityState(SystemUser systemUser)
        {
            if (_repositoryContext.GetEntityDescriptor(systemUser) != null)
            {
                return _repositoryContext.GetEntityDescriptor(systemUser).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }   
    }
}
