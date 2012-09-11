using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
using XERP.Domain.SecurityGroupDomain.Services;

namespace XERP.Domain.SecurityGroupDomain.Services
{
    public class SecurityGroupCodeSingletonRepository
    {
        private SecurityGroupCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SecurityGroupEntities(_rootUri);
        }

        private static SecurityGroupCodeSingletonRepository _instance;
        public static SecurityGroupCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SecurityGroupCodeSingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private SecurityGroupEntities _repositoryContext;

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes()
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupCodes
                               select q);
            return queryResult;
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(SecurityGroupCode securityGroupCodeQuerryObject)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SecurityGroupCodes
                              select q;

            if (!string.IsNullOrEmpty(securityGroupCodeQuerryObject.Code))
            {
                queryResult = queryResult.Where(q => q.Code.StartsWith(securityGroupCodeQuerryObject.Code.ToString()));
            }

            if (!string.IsNullOrEmpty(securityGroupCodeQuerryObject.Description))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupCodeQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(securityGroupCodeQuerryObject.SecurityGroupCodeID))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupCodeQuerryObject.SecurityGroupCodeID.ToString()));
            }

            return queryResult;
        }


        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodeByID(string securityGroupCodeID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupCodes
                               where q.SecurityGroupCodeID == securityGroupCodeID
                               select q);

            return queryResult;
        }

        public IEnumerable<SecurityGroupCode> Refresh(string autoIDs)
        {

            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SecurityGroupCode>("RefreshSecurityGroupCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SecurityGroupCode securityGroupCode)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupCode) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(securityGroupCode);
            }
        }

        public void AddToRepository(SecurityGroupCode securityGroupCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSecurityGroupCodes(securityGroupCode);
        }

        public void DeleteFromRepository(SecurityGroupCode securityGroupCode)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupCode) != null)
            {
                //if it exists in the db delete it from the db
                SecurityGroupEntities context = new SecurityGroupEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SecurityGroupCode deletedSecurityGroupCode = (from q in context.SecurityGroupCodes
                                          where q.SecurityGroupCodeID == securityGroupCode.SecurityGroupCodeID
                                          select q).SingleOrDefault();
                if (deletedSecurityGroupCode != null)
                {
                    context.DeleteObject(deletedSecurityGroupCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSecurityGroupCodeEntityState(securityGroupCode) != EntityStates.Detached)
                {
                    _repositoryContext.Detach(securityGroupCode);
                }
            }
        }

        public EntityStates GetSecurityGroupCodeEntityState(SecurityGroupCode securityGroupCode)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupCode) != null)
            {
                return _repositoryContext.GetEntityDescriptor(securityGroupCode).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }
    }
}