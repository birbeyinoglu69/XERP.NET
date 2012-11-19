using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain
{
    public class MenuItemTypeSingletonRepository
    {
        private MenuItemTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }

        private static MenuItemTypeSingletonRepository _instance;
        public static MenuItemTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MenuItemTypeSingletonRepository();
                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<MenuItemType> GetMenuItemTypes(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.MenuItemTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<MenuItemType> GetMenuItemTypes(MenuItemType securityGroupTypeQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.MenuItemTypes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty(securityGroupTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(securityGroupTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(securityGroupTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(securityGroupTypeQuerryObject.MenuItemTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(securityGroupTypeQuerryObject.MenuItemTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<MenuItemType> GetMenuItemTypeByID(string securityGroupTypeID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.MenuItemTypes
                               where q.MenuItemTypeID == securityGroupTypeID
                               where q.CompanyID == companyID
                               select q);

            return queryResult;
        }

        public IEnumerable<MenuItemType> Refresh(string autoIDs)
        {

            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<MenuItemType>("RefreshMenuItemType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(MenuItemType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(MenuItemType securityGroupType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToMenuItemTypes(securityGroupType);
        }

        public void DeleteFromRepository(MenuItemType securityGroupType)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupType) != null)
            {//if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                MenuItemType deletedMenuItemType = (from q in context.MenuItemTypes
                                          where q.MenuItemTypeID == securityGroupType.MenuItemTypeID
                                          select q).SingleOrDefault();
                if (deletedMenuItemType != null)
                {
                    context.DeleteObject(deletedMenuItemType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetMenuItemTypeEntityState(securityGroupType) != EntityStates.Detached)
                    _repositoryContext.Detach(securityGroupType);
            }
        }

        public EntityStates GetMenuItemTypeEntityState(MenuItemType securityGroupType)
        {
            if (_repositoryContext.GetEntityDescriptor(securityGroupType) != null)
                return _repositoryContext.GetEntityDescriptor(securityGroupType).State;
            else
                return EntityStates.Detached;
        }
    }
}
