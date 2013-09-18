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
                    _instance = new SecurityGroupSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private SecurityGroupEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

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

        public IEnumerable<SecurityGroup> GetSecurityGroups(SecurityGroup itemQuerryObject, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SecurityGroups
                              where q.CompanyID == companyID
                             select q; 
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(itemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.SecurityGroupTypeID))
                queryResult = queryResult.Where(q => q.SecurityGroupTypeID.StartsWith(itemQuerryObject.SecurityGroupTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.SecurityGroupCodeID))
                queryResult = queryResult.Where(q => q.SecurityGroupCodeID.StartsWith(itemQuerryObject.SecurityGroupCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<SecurityGroup> GetSecurityGroupByID(string itemID, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroups
                          where q.SecurityGroupID == itemID &&
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

        public void UpdateRepository(SecurityGroup item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(SecurityGroup item)
        {
            item.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSecurityGroups(item);
        }

        public void DeleteFromRepository(SecurityGroup item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                //if it exists in the db delete it from the db
                SecurityGroupEntities context = new SecurityGroupEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SecurityGroup deletedSecurityGroup = (from q in context.SecurityGroups
                                          where q.SecurityGroupID == item.SecurityGroupID
                                          select q).FirstOrDefault();
                if (deletedSecurityGroup != null)
                {
                    context.DeleteObject(deletedSecurityGroup);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetSecurityGroupEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetSecurityGroupEntityState(SecurityGroup item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
