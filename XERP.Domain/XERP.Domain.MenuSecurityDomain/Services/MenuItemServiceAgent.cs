using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
namespace XERP.Domain.MenuSecurityDomain.Services
{
    public class MenuItemServiceAgent : XERP.Domain.MenuSecurityDomain.Services.IMenuItemServiceAgent
    {
        #region Initialize Service
        public MenuItemServiceAgent()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            //this context will be used for read only gets...
            _context = new MenuSecurityEntities(_rootUri);
            _context.MergeOption = MergeOption.NoTracking;   
        }
        #endregion Initialize Service

        #region Properties
        private Uri _rootUri;
        private MenuSecurityEntities _context;        
        #endregion Properties

        #region Read Only Methods  No Repository Required
        public bool MenuItemRepositoryIsDirty()
        {
            return MenuItemSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool MenuItemTypeRepositoryIsDirty()
        {
            return MenuItemTypeSingletonRepository.Instance.RepositoryIsDirty();
        }

        public bool MenuItemCodeRepositoryIsDirty()
        {
            return MenuItemCodeSingletonRepository.Instance.RepositoryIsDirty();
        } 
        public IEnumerable<MenuItemType> GetMenuItemTypesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.MenuItemTypes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public IEnumerable<MenuItemCode> GetMenuItemCodesReadOnly(string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.MenuItemCodes
                               where q.CompanyID == companyID
                                select q);
            return queryResult;
        }

        public bool MenuItemExists(string menuItemID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.MenuItems
                           where q.MenuItemID == menuItemID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool MenuItemTypeExists(string menuItemTypeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.MenuItemTypes
                               where q.MenuItemTypeID == menuItemTypeID
                               where q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public bool MenuItemCodeExists(string menuItemCodeID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.MenuItemCodes
                               where q.MenuItemCodeID == menuItemCodeID &&
                               q.CompanyID == companyID
                               select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;

            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {//WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }
        #endregion Read Only Methods  No Repository Required

        #region MenuItem Repository CRUD
        public IEnumerable<MenuItem> RefreshMenuItem(string autoIDs)
        {
            return MenuItemSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<MenuItem> GetMenuItems(string companyID)
        {
            return MenuItemSingletonRepository.Instance.GetMenuItems(companyID);
        }

        public IEnumerable<MenuItem> GetMenuItems(MenuItem menuItemQuerryObject, string companyID)
        {
            return MenuItemSingletonRepository.Instance.GetMenuItems(menuItemQuerryObject, companyID);
        }

        public IEnumerable<MenuItem> GetMenuItemByID(string menuItemID, string companyID)
        {
            return MenuItemSingletonRepository.Instance.GetMenuItemByID(menuItemID, companyID);
        }

        public void CommitMenuItemRepository()
        {
            MenuItemSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateMenuItemRepository(MenuItem menuItem)
        {
            MenuItemSingletonRepository.Instance.UpdateRepository(menuItem);
        }

        public void AddToMenuItemRepository(MenuItem menuItem)
        {
            MenuItemSingletonRepository.Instance.AddToRepository(menuItem);
        }

        public void DeleteFromMenuItemRepository(MenuItem menuItem)
        {
            MenuItemSingletonRepository.Instance.DeleteFromRepository(menuItem);
        }

        public EntityStates GetMenuItemEntityState(MenuItem menuItem)
        {
            return MenuItemSingletonRepository.Instance.GetMenuItemEntityState(menuItem);
        }
        #endregion MenuItem Repository CRUD

        #region MenuItemType Repository CRUD
        public IEnumerable<MenuItemType> RefreshMenuItemType(string autoIDs)
        {
            return MenuItemTypeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<MenuItemType> GetMenuItemTypes(string companyID)
        {
            return MenuItemTypeSingletonRepository.Instance.GetMenuItemTypes(companyID);
        }

        public IEnumerable<MenuItemType> GetMenuItemTypes(MenuItemType menuItemTypeQuerryObject, string companyID)
        {
            return MenuItemTypeSingletonRepository.Instance.GetMenuItemTypes(menuItemTypeQuerryObject, companyID);
        }

        public IEnumerable<MenuItemType> GetMenuItemTypeByID(string menuItemTypeID, string companyID)
        {
            return MenuItemTypeSingletonRepository.Instance.GetMenuItemTypeByID(menuItemTypeID, companyID);
        }
        public void CommitMenuItemTypeRepository()
        {
            MenuItemTypeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateMenuItemTypeRepository(MenuItemType menuItemType)
        {
            MenuItemTypeSingletonRepository.Instance.UpdateRepository(menuItemType);
        }

        public void AddToMenuItemTypeRepository(MenuItemType menuItemType)
        {
            MenuItemTypeSingletonRepository.Instance.AddToRepository(menuItemType);
        }

        public void DeleteFromMenuItemTypeRepository(MenuItemType menuItemType)
        {
            MenuItemTypeSingletonRepository.Instance.DeleteFromRepository(menuItemType);
        }

        public EntityStates GetMenuItemTypeEntityState(MenuItemType menuItemType)
        {
            return MenuItemTypeSingletonRepository.Instance.GetMenuItemTypeEntityState(menuItemType);
        }

        #endregion MenuItemType Repository CRUD

        #region MenuItemCode Repository CRUD
        public IEnumerable<MenuItemCode> RefreshMenuItemCode(string autoIDs)
        {
            return MenuItemCodeSingletonRepository.Instance.Refresh(autoIDs);
        }

        public IEnumerable<MenuItemCode> GetMenuItemCodes(string companyID)
        {
            return MenuItemCodeSingletonRepository.Instance.GetMenuItemCodes(companyID);
        }

        public IEnumerable<MenuItemCode> GetMenuItemCodes(MenuItemCode menuItemCodeQuerryObject, string companyID)
        {
            return MenuItemCodeSingletonRepository.Instance.GetMenuItemCodes(menuItemCodeQuerryObject, companyID);
        }

        public IEnumerable<MenuItemCode> GetMenuItemCodeByID(string menuItemCodeID, string companyID)
        {
            return MenuItemCodeSingletonRepository.Instance.GetMenuItemCodeByID(menuItemCodeID, companyID);
        }
        public void CommitMenuItemCodeRepository()
        {
            MenuItemCodeSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateMenuItemCodeRepository(MenuItemCode menuItemCode)
        {
            MenuItemCodeSingletonRepository.Instance.UpdateRepository(menuItemCode);
        }

        public void AddToMenuItemCodeRepository(MenuItemCode menuItemCode)
        {
            MenuItemCodeSingletonRepository.Instance.AddToRepository(menuItemCode);
        }

        public void DeleteFromMenuItemCodeRepository(MenuItemCode menuItemCode)
        {
            MenuItemCodeSingletonRepository.Instance.DeleteFromRepository(menuItemCode);
        }

        public EntityStates GetMenuItemCodeEntityState(MenuItemCode menuItemCode)
        {
            return MenuItemCodeSingletonRepository.Instance.GetMenuItemCodeEntityState(menuItemCode);
        }

        #endregion MenuItemCode Repository CRUD
    }
}
