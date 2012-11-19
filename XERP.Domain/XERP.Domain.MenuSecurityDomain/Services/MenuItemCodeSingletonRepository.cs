using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain.Services
{
    public class MenuItemCodeSingletonRepository
    {
        private MenuItemCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }

        private static MenuItemCodeSingletonRepository _instance;
        public static MenuItemCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MenuItemCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<MenuItemCode> GetMenuItemCodes(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.MenuItemCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<MenuItemCode> GetMenuItemCodes(MenuItemCode securityGroupCodeQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.MenuItemCodes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty(securityGroupCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith(securityGroupCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty(securityGroupCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(securityGroupCodeQuerryObject.MenuItemCodeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupCodeQuerryObject.MenuItemCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<MenuItemCode> GetMenuItemCodeByID(string securityGroupCodeID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.MenuItemCodes
                               where q.MenuItemCodeID == securityGroupCodeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<MenuItemCode> Refresh(string autoIDs)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<MenuItemCode>("RefreshMenuItemCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(MenuItemCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(MenuItemCode securityGroupCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToMenuItemCodes(securityGroupCode);
        }

        public void DeleteFromRepository(MenuItemCode securityGroupCode)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupCode) != null)
            {
                //if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                MenuItemCode deletedMenuItemCode = (from q in context.MenuItemCodes
                                          where q.MenuItemCodeID == securityGroupCode.MenuItemCodeID
                                          select q).SingleOrDefault();
                if (deletedMenuItemCode != null)
                {
                    context.DeleteObject(deletedMenuItemCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetMenuItemCodeEntityState(securityGroupCode) != EntityStates.Detached)
                    _repositoryContext.Detach(securityGroupCode);
            }
        }

        public EntityStates GetMenuItemCodeEntityState(MenuItemCode securityGroupCode)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupCode) != null)
                return _repositoryContext.GetEntityDescriptor(securityGroupCode).State;
            else
                return EntityStates.Detached;
        }
    }
}