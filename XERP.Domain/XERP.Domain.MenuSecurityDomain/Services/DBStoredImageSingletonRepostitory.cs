using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.MenuSecurityDomain.MenuSecurityDataService;
using XERP.Domain.MenuSecurityDomain.Services;

namespace XERP.Domain.MenuSecurityDomain.Services
{
    public class DBStoredImageSingletonRepository 
    {
        private DBStoredImageSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new MenuSecurityEntities(_rootUri);
        }
        
        private static DBStoredImageSingletonRepository _instance;
        public static DBStoredImageSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DBStoredImageSingletonRepository();
                return _instance;
            }
        }

        private Uri _rootUri;
        private MenuSecurityEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<DBStoredImage> GetDBStoredImages(string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.DBStoredImages
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<DBStoredImage> GetDBStoredImages(DBStoredImage dBStoredImageQuerryObject, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.DBStoredImages
                              where q.CompanyID == companyID
                             select q;

            if (!string.IsNullOrEmpty(dBStoredImageQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(dBStoredImageQuerryObject.Description.ToString()));

            return queryResult;
        }

        public IEnumerable<DBStoredImage> GetDBStoredImageByID(string dBImageID, string companyID)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.DBStoredImages
                          where q.ImageID == dBImageID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<DBStoredImage> Refresh(string autoIDs)
        {
            _repositoryContext = new MenuSecurityEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<DBStoredImage>("RefreshDBStoredImage").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(DBStoredImage item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(DBStoredImage dBStoredImage)
        {
            dBStoredImage.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToDBStoredImages(dBStoredImage);
        }

        public void DeleteFromRepository(DBStoredImage dBStoredImage)
        {
            if (_repositoryContext.GetEntityDescriptor(dBStoredImage) != null)
            {//if it exists in the db delete it from the db
                MenuSecurityEntities context = new MenuSecurityEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                DBStoredImage deletedDBStoredImage = (from q in context.DBStoredImages
                                          where q.ImageID == dBStoredImage.ImageID
                                          select q).FirstOrDefault();
                if (deletedDBStoredImage != null)
                {
                    context.DeleteObject(deletedDBStoredImage);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetDBStoredImageEntityState(dBStoredImage) != EntityStates.Detached)
                    _repositoryContext.Detach(dBStoredImage);
            }
        }

        public EntityStates GetDBStoredImageEntityState(DBStoredImage dBStoredImage)
        {
            if (_repositoryContext.GetEntityDescriptor(dBStoredImage) != null)
                return _repositoryContext.GetEntityDescriptor(dBStoredImage).State;
            else
                return EntityStates.Detached;
        }   
    }
}
