using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;

using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using System.Windows.Media.Imaging;
using System.IO;
namespace XERP.Domain.MenuSecurityDomain.Services
{
    public class DBStoredImageServiceAgent : XERP.Domain.MenuSecurityDomain.Services.IDBStoredImageServiceAgent 
    {
        #region Initialize Service
        public DBStoredImageServiceAgent()
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
        public bool DBStoredImageRepositoryIsDirty()
        {
            return DBStoredImageSingletonRepository.Instance.RepositoryIsDirty();
        }


        public bool DBStoredImageExists(string dBImageID, string companyID)
        {
            _context.MergeOption = MergeOption.NoTracking;
            _context.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _context.DBStoredImages
                           where q.ImageID == dBImageID &&
                           q.CompanyID == companyID
                           select q).ToList();
            if (queryResult != null && queryResult.Count() > 0)
                return true;
            return false;
        }

        public IEnumerable<Temp> GetMetaData(string tableName)
        {
            //WCF Data Services does not allow for Complex query where you need to mine linked table data
            //with the same query so I have opted to use a webget sever side and do the query their...
            _context.IgnoreResourceNotFoundException = true;
            _context.MergeOption = MergeOption.NoTracking;
            var query = _context.CreateQuery<Temp>("GetMetaData").AddQueryOption("TableName", "'" + tableName + "'");
            return query;
        }

        public BitmapImage GetMenuItemImage(string imageID, string companyID)
        {
            System.Windows.Media.Imaging.BitmapImage wpfImg = new System.Windows.Media.Imaging.BitmapImage();
            try
            {
                DBStoredImage dbStoredImage = _context.DBStoredImages.First();
                _context.IgnoreResourceNotFoundException = true;
                _context.MergeOption = MergeOption.NoTracking;
                dbStoredImage = (from q in _context.DBStoredImages
                                 where q.ImageID == imageID &&
                                   q.CompanyID == companyID
                                 select q).FirstOrDefault();

                MemoryStream stream = new MemoryStream();
                stream.Write(dbStoredImage.StoredImage, 0, dbStoredImage.StoredImage.Length);

                //System.Windows.Media.Imaging.BitmapImage wpfImg = new System.Windows.Media.Imaging.BitmapImage();
                wpfImg.BeginInit();
                wpfImg.StreamSource = stream;
                wpfImg.EndInit();
            }//if image fails we will return the empty bitmapimage object
            catch 
            {//reset it as to have a new crisp empty one to return... as to hopefull not cause errors when trying to display a corrupted one...
                wpfImg = new System.Windows.Media.Imaging.BitmapImage();
            }
            return wpfImg;
        }
        #endregion Read Only Methods  No Repository Required

        #region DBStoredImage Repository CRUD
        public IEnumerable<DBStoredImage> RefreshDBStoredImage(string autoIDs)
        {
            return DBStoredImageSingletonRepository.Instance.Refresh(autoIDs);
        }
        public IEnumerable<DBStoredImage> GetDBStoredImages(string companyID)
        {
            return DBStoredImageSingletonRepository.Instance.GetDBStoredImages(companyID);
        }

        public IEnumerable<DBStoredImage> GetDBStoredImages(DBStoredImage dBStoredImageQuerryObject, string companyID)
        {
            return DBStoredImageSingletonRepository.Instance.GetDBStoredImages(dBStoredImageQuerryObject, companyID);
        }

        public IEnumerable<DBStoredImage> GetDBStoredImageByID(string dBImageID, string companyID)
        {
            return DBStoredImageSingletonRepository.Instance.GetDBStoredImageByID(dBImageID, companyID);
        }

        public void CommitDBStoredImageRepository()
        {
            DBStoredImageSingletonRepository.Instance.CommitRepository();
        }

        public void UpdateDBStoredImageRepository(DBStoredImage dBStoredImage)
        {
            DBStoredImageSingletonRepository.Instance.UpdateRepository(dBStoredImage);
        }

        public void AddToDBStoredImageRepository(DBStoredImage dBStoredImage)
        {
            DBStoredImageSingletonRepository.Instance.AddToRepository(dBStoredImage);
        }

        public void DeleteFromDBStoredImageRepository(DBStoredImage dBStoredImage)
        {
            DBStoredImageSingletonRepository.Instance.DeleteFromRepository(dBStoredImage);
        }

        public EntityStates GetDBStoredImageEntityState(DBStoredImage dBStoredImage)
        {
            return DBStoredImageSingletonRepository.Instance.GetDBStoredImageEntityState(dBStoredImage);
        }
        #endregion DBStoredImage Repository CRUD

    }
}
