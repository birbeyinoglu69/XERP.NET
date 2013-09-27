using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.PlantDomain.PlantDataService;
using XERP.Domain.PlantDomain.Services;

namespace XERP.Domain.PlantDomain
{
    public class PlantSingletonRepository
    {
        private PlantSingletonRepository() 
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new PlantEntities(_rootUri);
        }
        
        private static PlantSingletonRepository _instance;
        public static PlantSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlantSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private PlantEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<Plant> GetPlants(string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Plants
                               where q.CompanyID == companyID
                             select q);
            return queryResult;
        }

        public IEnumerable<Plant> GetPlants(Plant itemQuerryObject, string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.Plants
                              where q.CompanyID == companyID
                             select q; 
            if  (!string.IsNullOrEmpty(itemQuerryObject.Name))
                queryResult = queryResult.Where(q => q.Name.StartsWith(itemQuerryObject.Name.ToString())); 

            if (!string.IsNullOrEmpty(itemQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith(itemQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.PlantTypeID))
                queryResult = queryResult.Where(q => q.PlantTypeID.StartsWith(itemQuerryObject.PlantTypeID.ToString()));

            if (!string.IsNullOrEmpty(itemQuerryObject.PlantCodeID))
                queryResult = queryResult.Where(q => q.PlantCodeID.StartsWith(itemQuerryObject.PlantCodeID.ToString()));

            return queryResult;
        }

        public IEnumerable<Plant> GetPlantByID(string itemID, string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.Plants
                          where q.PlantID == itemID &&
                          q.CompanyID == companyID
                          select q);
            return queryResult;
        }

        public IEnumerable<Plant> Refresh(string autoIDs)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<Plant>("RefreshPlant").AddQueryOption("autoIDs", "'" + autoIDs + "'");
                
            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(Plant item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                item.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                item.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(item);
            }
        }

        public void AddToRepository(Plant item)
        {
            item.CompanyID = XERP.Client.ClientSessionSingleton.Instance.CompanyID;
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToPlants(item);
        }

        public void DeleteFromRepository(Plant item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
            {
                //if it exists in the db delete it from the db
                PlantEntities context = new PlantEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                Plant deletedPlant = (from q in context.Plants
                                          where q.PlantID == item.PlantID
                                          select q).FirstOrDefault();
                if (deletedPlant != null)
                {
                    context.DeleteObject(deletedPlant);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if(GetPlantEntityState(item) != EntityStates.Detached)
                    _repositoryContext.Detach(item);
            }
        }

        public EntityStates GetPlantEntityState(Plant item)
        {
            if (_repositoryContext.GetEntityDescriptor(item) != null)
                return _repositoryContext.GetEntityDescriptor(item).State;
            else
                return EntityStates.Detached;
        }   
    }
}
