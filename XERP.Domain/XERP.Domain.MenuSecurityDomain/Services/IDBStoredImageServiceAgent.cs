using System;
namespace XERP.Domain.MenuSecurityDomain.Services
{
    public interface IDBStoredImageServiceAgent
    {
        System.Windows.Media.Imaging.BitmapImage GetMenuItemImage(string imageID, string companyID);
        void AddToDBStoredImageRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage dBStoredImage);
        void CommitDBStoredImageRepository();
        bool DBStoredImageExists(string dBImageID, string companyID);
        bool DBStoredImageRepositoryIsDirty();
        void DeleteFromDBStoredImageRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage dBStoredImage);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage> GetDBStoredImageByID(string dBImageID, string companyID);
        System.Data.Services.Client.EntityStates GetDBStoredImageEntityState(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage dBStoredImage);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage> GetDBStoredImages(string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage> GetDBStoredImages(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage dBStoredImageQuerryObject, string companyID);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.Temp> GetMetaData(string tableName);
        System.Collections.Generic.IEnumerable<XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage> RefreshDBStoredImage(string autoIDs);
        void UpdateDBStoredImageRepository(XERP.Domain.MenuSecurityDomain.MenuSecurityDataService.DBStoredImage dBStoredImage);
    }
}
