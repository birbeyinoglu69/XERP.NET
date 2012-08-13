using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using XERP.Server.DAL.CompanyDAL;
using System.Data.Metadata.Edm;
using ExtensionMethods;
using System.Data.Services.Common;
namespace XERP.Server.DAL.CompanyDAL
{
    //public partial class Company :EntityObject
    //{
        
    //}
}

//we use the extension Method it allows to extend methods to existing entity objects...
//so that as long as use add the using ExtensionMethods you can utilize the methods below with a . reference...
//as if the the methods are part of the existing objects that they are referenced to by the this declaration
//in the parameter...
namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string GetPropertyValue(this Company myObj, string propertyName)
        {
            var propInfo = typeof(Company).GetProperty(propertyName);

            if (propInfo != null)
            {
                return propInfo.GetValue(myObj, null).ToString();
            }
            else
            {
                return string.Empty;
            }
        }
 
        public static List<Temp> GetMetaData(this Company entityObject)
        {
            XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (CompanyEntities ctx = new CompanyEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.Companies.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name)
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.ShortChar_1 = queryResult.TypeUsage.EdmType.Name;
                        if(queryResult.TypeUsage.EdmType.Name == "String")
                        {
                            temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        }
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        //All senior tables(Companies, Parts, Orders, ect...) tend to have a generic table called TableNameTypes 
        //i.e. CompanyTypes PartTypes, OrderTypes...
        //So this will constitute the meta data for that Type table...
        //Xerp attempts to use generic naming where possible to allow for cloning...
        public static List<Temp> GetMetaData(this CompanyType entityObject)
        {
            XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (CompanyEntities ctx = new CompanyEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.CompanyTypes.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name)
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.ShortChar_1 = queryResult.TypeUsage.EdmType.Name;
                        if (queryResult.TypeUsage.EdmType.Name == "String")
                        {
                            temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        }
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        //All senior tables(Companies, Parts, Orders, ect...) tend to have a generic table called TableNameCodes 
        //i.e. CompanyCodes PartCodes, OrderCodes...
        //So this will constitute the meta data for that Code table...
        //Xerp attempts to use generic naming where possible to allow for cloning...
        public static List<Temp> GetMetaData(this CompanyCode entityObject)
        {
            XERP.Server.DAL.CompanyDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (CompanyEntities ctx = new CompanyEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.CompanyCodes.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name)
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.ShortChar_1 = queryResult.TypeUsage.EdmType.Name;
                        if (queryResult.TypeUsage.EdmType.Name == "String")
                        {
                            temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        }
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }
    }
}
