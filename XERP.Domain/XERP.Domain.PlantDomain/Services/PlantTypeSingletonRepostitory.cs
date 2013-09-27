using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.PlantDomain.PlantDataService;
using XERP.Domain.PlantDomain.Services;

namespace XERP.Domain.PlantDomain
{
    public class PlantTypeSingletonRepository
    {
        private PlantTypeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new PlantEntities(_rootUri);
        }

        private static PlantTypeSingletonRepository _instance;
        public static PlantTypeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlantTypeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private PlantEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<PlantType> GetPlantTypes(string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.PlantTypes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<PlantType> GetPlantTypes(PlantType itemTypeQuerryObject, string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.PlantTypes
                              where q.CompanyID == companyID
                              select q;
            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Type))
                queryResult = queryResult.Where(q => q.Type.StartsWith(itemTypeQuerryObject.Type.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemTypeQuerryObject.PlantTypeID))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemTypeQuerryObject.PlantTypeID.ToString()));

            return queryResult;
        }


        public IEnumerable<PlantType> GetPlantTypeByID(string itemTypeID, string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.PlantTypes
                               where q.PlantTypeID == itemTypeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<PlantType> Refresh(string autoIDs)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<PlantType>("RefreshPlantType").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(PlantType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {
                itemType.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemType.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemType);
            }
        }

        public void AddToRepository(PlantType itemType)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToPlantTypes(itemType);
        }

        public void DeleteFromRepository(PlantType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
            {//if it exists in the db delete it from the db
                PlantEntities context = new PlantEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                PlantType deletedPlantType = (from q in context.PlantTypes
                                          where q.PlantTypeID == itemType.PlantTypeID
                                          select q).FirstOrDefault();
                if (deletedPlantType != null)
                {
                    context.DeleteObject(deletedPlantType);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetPlantTypeEntityState(itemType) != EntityStates.Detached)
                    _repositoryContext.Detach(itemType);
            }
        }

        public EntityStates GetPlantTypeEntityState(PlantType itemType)
        {
            if (_repositoryContext.GetEntityDescriptor(itemType) != null)
                return _repositoryContext.GetEntityDescriptor(itemType).State;
            else
                return EntityStates.Detached;
        }
    }
}
