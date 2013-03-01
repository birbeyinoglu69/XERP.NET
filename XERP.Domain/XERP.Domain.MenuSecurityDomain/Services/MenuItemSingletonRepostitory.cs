using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain
{
    public class MenuItemSingletonRepository
    {
        private MenuItemSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }
        
        private static MenuItemSingletonRepository _instance;
        public static MenuItemSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MenuItemSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<MenuItem> GetMenuItems(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.MenuItems.Expand("MenuSecurities/SecurityGroup")
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<MenuItem> GetMenuItems(MenuItem menuItemQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.MenuItems.Expand("MenuSecurities/SecurityGroup")
                              where q.CompanyID == companyID
                             select q;
            
            if  (!string.IsNullOrEmpty(menuItemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(menuItemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(menuItemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(menuItemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(menuItemQuerryObject.MenuItemTypeID))
                queryResult = queryResult.Where(q => q.MenuItemTypeID.StartsWith(menuItemQuerryObject.MenuItemTypeID.ToString()));

            if (!string.IsNullOrEmpty(menuItemQuerryObject.MenuItemCodeID))
                queryResult = queryResult.Where(q => q.MenuItemCodeID.StartsWith(menuItemQuerryObject.MenuItemCodeID.ToString()));
            return queryResult;
        }

        public IEnumerable<MenuItem> GetMenuItemByID(string menuItemID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.MenuItems.Expand("MenuSecurities/SecurityGroup")
                          where q.MenuItemID == menuItemID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<MenuItem> Refresh(string autoIDs)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<MenuItem>("RefreshMenuItem").
                Expand("MenuSecurities/SecurityGroup").
                AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(MenuItem item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(MenuItem menuItem)
        {
            menuItem.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToMenuItems(menuItem);
        }

        public void DeleteFromRepository(MenuItem menuItem)
        {
            if (_repositoryContext.GetEntityDescriptor(menuItem) != null)
            {//if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                MenuItem deletedMenuItem = (from q in context.MenuItems
                                          where q.MenuItemID == menuItem.MenuItemID
                                          select q).SingleOrDefault();
                if (deletedMenuItem != null)
                {
                    context.DeleteObject(deletedMenuItem);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetMenuItemEntityState(menuItem) != EntityStates.Detached)
                    _repositoryContext.Detach(menuItem);
            }
        }

        public EntityStates GetMenuItemEntityState(MenuItem menuItem)
        {
            if (_repositoryContext.GetEntityDescriptor(menuItem) != null)
                return _repositoryContext.GetEntityDescriptor(menuItem).State;
            else
                return EntityStates.Detached;
        }   
    }
}
