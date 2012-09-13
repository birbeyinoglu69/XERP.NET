using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.SecurityGroupDomain.SecurityGroupDataService;
using XERP.Domain.SecurityGroupDomain.Services;

namespace XERP.Domain.SecurityGroupDomain
{
    public class SecurityGroupSingletonRepository
    {
        private SecurityGroupSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new SecurityGroupEntities(_rootUri);
        }
        
        private static SecurityGroupSingletonRepository _instance;
        public static SecurityGroupSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SecurityGroupSingletonRepository();
                }
                return _instance;
            }
        }

        private Uri _rootUri;
        private SecurityGroupEntities _repositoryContext;

        public IEnumerable<SecurityGroup> GetSecurityGroups(string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroups
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<SecurityGroup> GetSecurityGroups(SecurityGroup securityGroupQuerryObject, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SecurityGroups
                              where q.CompanyID == companyID
                             select q;
            
            if  (!string.IsNullOrEmpty(securityGroupQuerryObject.Name))
            {
                queryResult = queryResult.Where(q => q.Name.StartsWith(securityGroupQuerryObject.Name.ToString())); 
            }

            if (!string.IsNullOrEmpty(securityGroupQuerryObject.Description))
            {
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupQuerryObject.Description.ToString()));
            }

            if (!string.IsNullOrEmpty(securityGroupQuerryObject.SecurityGroupTypeID))
            {
                queryResult = queryResult.Where(q => q.SecurityGroupTypeID.StartsWith(securityGroupQuerryObject.SecurityGroupTypeID.ToString()));
            }

            if (!string.IsNullOrEmpty(securityGroupQuerryObject.SecurityGroupCodeID))
            {
                queryResult = queryResult.Where(q => q.SecurityGroupCodeID.StartsWith(securityGroupQuerryObject.SecurityGroupCodeID.ToString()));
            }
            return queryResult;
        }

        public IEnumerable<SecurityGroup> GetSecurityGroupByID(string securityGroupID, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroups
                          where q.SecurityGroupID == securityGroupID &&
                          q.CompanyID == companyID
                          select q);
            
            return queryResult;
        }

        public IEnumerable<SecurityGroup> Refresh(string autoIDs)
        {

            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<SecurityGroup>("RefreshSecurityGroup").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(SecurityGroup securityGroup)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroup) != null)
            {
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(securityGroup);
            }
        }

        public void AddToRepository(SecurityGroup securityGroup)
        {
            securityGroup.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSecurityGroups(securityGroup);
        }

        public void DeleteFromRepository(SecurityGroup securityGroup)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroup) != null)
            {
                //if it exists in the db delete it from the db
                SecurityGroupEntities context = new SecurityGroupEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SecurityGroup deletedSecurityGroup = (from q in context.SecurityGroups
                                          where q.SecurityGroupID == securityGroup.SecurityGroupID
                                          select q).SingleOrDefault();
                if (deletedSecurityGroup != null)
                {
                    context.DeleteObject(deletedSecurityGroup);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetSecurityGroupEntityState(securityGroup) != EntityStates.Detached)
                {
                    _repositoryContext.Detach(securityGroup);
                }
            }
        }

        public EntityStates GetSecurityGroupEntityState(SecurityGroup securityGroup)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroup) != null)
            {
                return _repositoryContext.GetEntityDescriptor(securityGroup).State;
            }
            else
            {
                return EntityStates.Detached;
            }
        }   
    }
}
