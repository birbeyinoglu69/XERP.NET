using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
using XERP.Domain.SecurityGroupDomain.Services;

namespace XERP.Domain.SecurityGroupDomain
{
    public class SecurityGroupTypeSingletonRepository
    {
        private SecurityGroupTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SecurityGroupEntities(_rootUri);
        }

        private static SecurityGroupTypeSingletonRepository _instance;
        public static SecurityGroupTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SecurityGroupTypeSingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private SecurityGroupEntities _repositoryContext;

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType securityGroupTypeQuerryObject, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SecurityGroupTypes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty(securityGroupTypeQuerryObject.Type))
            {
                queryResult = queryResult.Where(q => q.Type.StartsWith(securityGroupTypeQuerryObject.Type.ToString()));
            }

            if (!string.IsNullOrEmpty(securityGroupTypeQuerryObject.Description))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupTypeQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(securityGroupTypeQuerryObject.SecurityGroupTypeID))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupTypeQuerryObject.SecurityGroupTypeID.ToString()));
            }

            return queryResult;
        }


        public IEnumerable<SecurityGroupType> GetSecurityGroupTypeByID(string securityGroupTypeID, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupTypes
                               where q.SecurityGroupTypeID == securityGroupTypeID
                               where q.CompanyID == companyID
                               select q);

            return queryResult;
        }

        public IEnumerable<SecurityGroupType> Refresh(string autoIDs)
        {

            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SecurityGroupType>("RefreshSecurityGroupType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SecurityGroupType securityGroupType)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupType) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(securityGroupType);
            }
        }

        public void AddToRepository(SecurityGroupType securityGroupType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSecurityGroupTypes(securityGroupType);
        }

        public void DeleteFromRepository(SecurityGroupType securityGroupType)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupType) != null)
            {
                //if it exists in the db delete it from the db
                SecurityGroupEntities context = new SecurityGroupEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SecurityGroupType deletedSecurityGroupType = (from q in context.SecurityGroupTypes
                                          where q.SecurityGroupTypeID == securityGroupType.SecurityGroupTypeID
                                          select q).SingleOrDefault();
                if (deletedSecurityGroupType != null)
                {
                    context.DeleteObject(deletedSecurityGroupType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSecurityGroupTypeEntityState(securityGroupType) != EntityStates.Detached)
                {
                    _repositoryContext.Detach(securityGroupType);
                }
            }
        }

        public EntityStates GetSecurityGroupTypeEntityState(SecurityGroupType securityGroupType)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupType) != null)
            {
                return _repositoryContext.GetEntityDescriptor(securityGroupType).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }
    }
}
