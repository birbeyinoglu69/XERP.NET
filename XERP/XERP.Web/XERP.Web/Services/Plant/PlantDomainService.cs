//If Schema changes are required all the canned code below will be lost...
//Do not modify this Class use the partial extended class
//If one of the canned CRUD Methods below needs modification
//Cut and paste it to the extended Partial Class and modify their
namespace XERP.Web.Services.Plant
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using XERP.Web.Models.Plant;
    using System.Configuration;
    using System.Data.EntityClient;


    // Implements application logic using the PlantEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class PlantDomainService : LinqToEntitiesDomainService<PlantEntities>
    {
        
        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Addresses' query.
        public IQueryable<Address> GetAddresses()
        {
            return this.ObjectContext.Addresses;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Companies' query.
        public IQueryable<Company> GetCompanies()
        {
            return this.ObjectContext.Companies;
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Plants' query.
        public IQueryable<Plant> GetPlants()
        {
            return this.ObjectContext.Plants;
        }

        public void InsertPlant(Plant plant)
        {
            if ((plant.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(plant, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Plants.AddObject(plant);
            }
        }

        public void UpdatePlant(Plant currentPlant)
        {
            this.ObjectContext.Plants.AttachAsModified(currentPlant, this.ChangeSet.GetOriginal(currentPlant));
        }

        public void DeletePlant(Plant plant)
        {
            if ((plant.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(plant, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Plants.Attach(plant);
                this.ObjectContext.Plants.DeleteObject(plant);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PlantCodes' query.
        public IQueryable<PlantCode> GetPlantCodes()
        {
            return this.ObjectContext.PlantCodes;
        }

        public void InsertPlantCode(PlantCode plantCode)
        {
            if ((plantCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(plantCode, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PlantCodes.AddObject(plantCode);
            }
        }

        public void UpdatePlantCode(PlantCode currentPlantCode)
        {
            this.ObjectContext.PlantCodes.AttachAsModified(currentPlantCode, this.ChangeSet.GetOriginal(currentPlantCode));
        }

        public void DeletePlantCode(PlantCode plantCode)
        {
            if ((plantCode.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(plantCode, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PlantCodes.Attach(plantCode);
                this.ObjectContext.PlantCodes.DeleteObject(plantCode);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'PlantTypes' query.
        public IQueryable<PlantType> GetPlantTypes()
        {
            return this.ObjectContext.PlantTypes;
        }

        public void InsertPlantType(PlantType plantType)
        {
            if ((plantType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(plantType, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PlantTypes.AddObject(plantType);
            }
        }

        public void UpdatePlantType(PlantType currentPlantType)
        {
            this.ObjectContext.PlantTypes.AttachAsModified(currentPlantType, this.ChangeSet.GetOriginal(currentPlantType));
        }

        public void DeletePlantType(PlantType plantType)
        {
            if ((plantType.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(plantType, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PlantTypes.Attach(plantType);
                this.ObjectContext.PlantTypes.DeleteObject(plantType);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Warehouses' query.
        public IQueryable<Warehouse> GetWarehouses()
        {
            return this.ObjectContext.Warehouses;
        }
    }

}


