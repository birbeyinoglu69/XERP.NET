using System;
using System.Collections.Generic;
using System.Linq;
using XERP.Server.DAL.UdListDAL;
using System.Data.Metadata.Edm;
//we use the extension Method it allows to extend methods to existing entity objects...
//so that as long as use add the using ExtensionMethods you can utilize the methods below with a . reference...
//as if the the methods are part of the existing objects that they are referenced to by the this declaration
//in the parameter...
namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static List<Temp> GetMetaData(this UdList entityObject)
        {
            XERP.Server.DAL.UdListDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (UdListEntities ctx = new UdListEntities(dalUtility.EntityConectionString))
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

        public static List<Temp> GetMetaData(this UdListItem entityObject)
        {
            XERP.Server.DAL.UdListDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (UdListEntities ctx = new UdListEntities(dalUtility.EntityConectionString))
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
