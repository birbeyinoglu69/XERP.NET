using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Services.Client;
using XERP.Domain.PlantDomain.PlantDataService;
using XERP.Domain.PlantDomain.Services;

namespace XERP.Domain.PlantDomain.Services
{
    public class PlantCodeSingletonRepository
    {
        private PlantCodeSingletonRepository()
        {
            ServiceUtility serviceUtility = new ServiceUtility();
            _rootUri = serviceUtility.BaseUri;
            _repositoryContext = new PlantEntities(_rootUri);
        }

        private static PlantCodeSingletonRepository _instance;
        public static PlantCodeSingletonRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PlantCodeSingletonRepository();

                return _instance;
            }
        }

        private Uri _rootUri;
        private PlantEntities _repositoryContext;

        public bool RepositoryIsDirty()
        {
            return _repositoryContext.Entities.Any(ed => ed.State != EntityStates.Unchanged);
        }

        public IEnumerable<PlantCode> GetPlantCodes(string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.PlantCodes
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<PlantCode> GetPlantCodes(PlantCode itemCodeQuerryObject, string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = from q in _repositoryContext.PlantCodes
                              where q.CompanyID == companyID
                              select q;

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Code))
                queryResult = queryResult.Where(q => q.Code.StartsWith( itemCodeQuerryObject.Code.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.Description))
                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.Description.ToString()));

            if (!string.IsNullOrEmpty( itemCodeQuerryObject.PlantCodeID))

                queryResult = queryResult.Where(q => q.Description.StartsWith( itemCodeQuerryObject.PlantCodeID.ToString()));

            return queryResult;
        }


        public IEnumerable<PlantCode> GetPlantCodeByID(string itemCodeID, string companyID)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;
            var queryResult = (from q in _repositoryContext.PlantCodes
                               where q.PlantCodeID == itemCodeID
                               where q.CompanyID == companyID
                               select q);
            return queryResult;
        }

        public IEnumerable<PlantCode> Refresh(string autoIDs)
        {
            _repositoryContext = new PlantEntities(_rootUri);
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.IgnoreResourceNotFoundException = true;

            var queryResult = _repositoryContext.CreateQuery<PlantCode>("RefreshPlantCode").AddQueryOption("autoIDs", "'" + autoIDs + "'");

            return queryResult;
        }

        public void CommitRepository()
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.SaveChanges();
        }

        public void UpdateRepository(PlantCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor(itemCode) != null)
            {
                itemCode.LastModifiedBy = XERP.Client.ClientSessionSingleton.Instance.SystemUserID;
                itemCode.LastModifiedByDate = DateTime.Now;
                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                _repositoryContext.UpdateObject(itemCode);
            }
        }

        public void AddToRepository(PlantCode itemCode)
        {
            _repositoryContext.MergeOption = MergeOption.AppendOnly;
            _repositoryContext.AddToPlantCodes( itemCode);
        }

        public void DeleteFromRepository(PlantCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
            {//if it exists in the db delete it from the db
                PlantEntities context = new PlantEntities(_rootUri);
                context.MergeOption = MergeOption.AppendOnly;
                context.IgnoreResourceNotFoundException = true;
                PlantCode deletedPlantCode = (from q in context.PlantCodes
                                          where q.PlantCodeID == itemCode.PlantCodeID
                                          select q).FirstOrDefault();
                if (deletedPlantCode != null)
                {
                    context.DeleteObject(deletedPlantCode);
                    context.SaveChanges();
                }
                context = null;

                _repositoryContext.MergeOption = MergeOption.AppendOnly;
                //if it is being tracked remove it...
                if (GetPlantCodeEntityState( itemCode) != EntityStates.Detached)
                    _repositoryContext.Detach( itemCode);
            }
        }

        public EntityStates GetPlantCodeEntityState(PlantCode itemCode)
        {
            if (_repositoryContext.GetEntityDescriptor( itemCode) != null)
                return _repositoryContext.GetEntityDescriptor( itemCode).State;
            else
                return EntityStates.Detached;
        }
    }
}