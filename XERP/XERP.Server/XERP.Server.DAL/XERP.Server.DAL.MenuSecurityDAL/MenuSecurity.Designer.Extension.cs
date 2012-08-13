using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using XERP.Server.DAL.MenuSecurityDAL;
using System.Data.Metadata.Edm;
using ExtensionMethods;
using System.Data.Services.Common;

namespace XERP.Server.DAL.MenuSecurityDAL
{

}

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static List<Temp> GetMetaData(this MenuItem entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.MenuItems.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                  from query in (meta as EntityType).Properties
                                          .Where(p => p.DeclaringType.Name == entityObject.GetType().Name
                                                  
                                                  && p.TypeUsage.EdmType.Name == "String")
                                  select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this MenuItemType entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.MenuItemTypes.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this MenuItemCode entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.MenuItemCodes.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this ExecutableProgram entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.ExecutablePrograms.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this ExecutableProgramType entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.ExecutableProgramTypes.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this ExecutableProgramCode entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.ExecutableProgramCodes.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this MenuSecurity entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.MenuSecurities.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
                        temp.Bool_1 = false; //we use this as a error trigger false = not an error...
                        tempList.Add(temp);
                        id++;
                    }
                }
            }
            return tempList;
        }

        public static List<Temp> GetMetaData(this SecurityGroup entityObject)
        {
            XERP.Server.DAL.MenuSecurityDAL.DALUtility dalUtility = new DALUtility();
            List<Temp> tempList = new List<Temp>();
            int id = 0;
            using (MenuSecurityEntities ctx = new MenuSecurityEntities(dalUtility.EntityConectionString))
            {

                var c = ctx.SecurityGroups.FirstOrDefault();
                var queryResults = from meta in ctx.MetadataWorkspace.GetItems(DataSpace.CSpace)
                                                        .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                                   from query in (meta as EntityType).Properties
                                           .Where(p => p.DeclaringType.Name == entityObject.GetType().Name

                                                   && p.TypeUsage.EdmType.Name == "String")
                                   select query;

                if (queryResults.Count() > 0)
                {
                    foreach (var queryResult in queryResults.ToList())
                    {
                        Temp temp = new Temp();
                        temp.ID = id;
                        temp.Name = queryResult.Name.ToString();
                        temp.Int_1 = Convert.ToInt32(queryResult.TypeUsage.Facets["MaxLength"].Value);
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
