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
                    _instance = new SecurityGroupCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private SecurityGroupEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodes(SecurityGroupCode itemCodeQuerryObject, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.SecurityGroupCodes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith( itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.SecurityGroupCodeID))

                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.SecurityGroupCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<SecurityGroupCode> GetSecurityGroupCodeByID(string itemCodeID, string companyID)
        {
            _repositoryContext = new SecurityGroupEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.SecurityGroupCodes
                               where q.SecurityGroupCodeID == itemCodeID
                               where q.CompanyID == companyID
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

        public void UpdateRepository(SecurityGroupCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(SecurityGroupCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToSecurityGroupCodes( itemCode);
        }

        public void DeleteFromRepository(SecurityGroupCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
            {//if it exists in the db delete it from the db
                SecurityGroupEntities context = new SecurityGroupEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                SecurityGroupCode deletedSecurityGroupCode = (from q in context.SecurityGroupCodes
                                          where q.SecurityGroupCodeID == itemCode.SecurityGroupCodeID
                                          select q).SingleOrDefault();
                if (deletedSecurityGroupCode != null)
                {
                    context.DeleteObject(deletedSecurityGroupCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetSecurityGroupCodeEntityState( itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach( itemCode);
            }
        }

        public EntityStates GetSecurityGroupCodeEntityState(SecurityGroupCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
                return _repositoryContext.GetEntityDescriptor( itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}