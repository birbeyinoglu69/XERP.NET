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
                    _instance = new SecurityGroupTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private SecurityGroupEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

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

        public IEnumerable<SecurityGroupType> GetSecurityGroupTypes(SecurityGroupType itemTypeQuerryObject, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SecurityGroupTypes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.SecurityGroupTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.SecurityGroupTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<SecurityGroupType> GetSecurityGroupTypeByID(string itemTypeID, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupTypes
                               where q.SecurityGroupTypeID == itemTypeID
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

        public void UpdateRepository(SecurityGroupType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(SecurityGroupType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSecurityGroupTypes(itemType);
        }

        public void DeleteFromRepository(SecurityGroupType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {//if it exists in the db delete it from the db
                SecurityGroupEntities context = new SecurityGroupEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SecurityGroupType deletedSecurityGroupType = (from q in context.SecurityGroupTypes
                                          where q.SecurityGroupTypeID == itemType.SecurityGroupTypeID
                                          select q).SingleOrDefault();
                if (deletedSecurityGroupType != null)
                {
                    context.DeleteObject(deletedSecurityGroupType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSecurityGroupTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetSecurityGroupTypeEntityState(SecurityGroupType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
