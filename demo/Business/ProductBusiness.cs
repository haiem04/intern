using demo.CRUD;
using demo.DTO;
using demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Web;

namespace demo.Business
{
    public class ProductBusiness
    {
        private static internShipEntities db = new internShipEntities();

        public static dynamic FilterProduct(JObject filterQuery) 
        {
            internShipEntities db = new internShipEntities();
            IQueryable<SanPham> query = db.SanPhams.AsQueryable();
            List<SanPhamDTO> filteredItems = null;

            if (filterQuery == null)
            {
                filteredItems = query.ToList().Select(x => x.ToDTO()).ToList();
            }
            else
            {
                var conditions = new List<string>();
             
                foreach (var property in filterQuery.Properties())
                {
                    string propName = property.Name;
                    JToken token = property.Value;
                    JTokenType typeOfToken = property.Value.Type;
                    string condition = "";

                    if (typeOfToken == JTokenType.Object)
                    {
                        condition = objectConditon(propName, token);
                        conditions.Add(condition);
                        break;
                    }
                    else if (typeOfToken == JTokenType.Array)
                    {
                        List<string> propertyValues = token.ToObject<List<string>>();
                        condition = $"{propName} IN ({string.Join(",", propertyValues)})";
                        conditions.Add(condition);

                        break;

                    }
                    else
                    {
                        condition = $"{propName} = \"{property.Value}\"";
                        conditions.Add(condition);
                        break;
                    }


                }

                string combinedConditions = string.Join(" AND ", conditions);
                query = query.Where(combinedConditions);
                filteredItems = query.ToList().Select(x => x.ToDTO()).ToList();
            }

            int pageNumber = filterQuery?["currentPage"]?.ToObject<int>() ?? 1;
            int pageSize = filterQuery?["pageSize"]?.ToObject<int>() ?? 5;
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var result = new
            {
                currentPage = pageNumber,
                pageSize = pageSize,
                totalItem = totalItems,
                totalPages = totalPages,
                filteredItems = filteredItems.Cast<object>().ToList(),
                filterQuery = filterQuery
            };

            return result;
        }


        public static string objectConditon(string propName, JToken token)
        {
            JObject obj = token.ToObject<JObject>();
            List<string> conditions = new List<string>();

            foreach (var property in obj.Properties())
            {
                switch (property.Name)
                {
                    case "eq":
                        conditions.Add($"{propName} = \"{property.Value}\"");
                        break;
                    case "lt":
                        conditions.Add($"{propName} < \"{property.Value}\"");
                        break;
                    case "lte":
                        conditions.Add($"{propName} <= \"{property.Value}\"");
                        break;
                    case "gt":
                        conditions.Add($"{propName} > \"{property.Value}\"");
                        break;
                    case "gte":
                        conditions.Add($"{propName} >= \"{property.Value}\"");
                        break;
                    default:
                        conditions.Add("");
                        break;

                }
            }
            string combie = string.Join(" AND ", conditions);

            return combie;
        }

        public static SanPhamDTO CreateProduct(JObject data)
        {
            SanPhamDTO spDTO = data.ToObject<SanPhamDTO>();
            SanPham sp = spDTO.ToModel();
            try
            {
                db.SanPhams.Add(sp);
                db.SaveChanges();
                return sp.ToDTO();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static SanPhamDTO UpdateProduct(JObject data)
        {
            var keyName = CRUDExtensions.GetPrimaryKeyName<SanPham>();
            SanPhamDTO dto = data.ToObject<SanPhamDTO>();
            PropertyInfo primaryKeyPropertyT = typeof(SanPham).GetProperty(keyName);
            PropertyInfo primaryKeyPropertyTDto = typeof(SanPhamDTO).GetProperty(keyName);
            object primaryKeyValueTDto = primaryKeyPropertyTDto.GetValue(dto);
            SanPham existingEntity = db.SanPhams.ToList().FirstOrDefault(x => primaryKeyPropertyT.GetValue(x).ToString() == primaryKeyValueTDto.ToString());

            if (existingEntity == null)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(SanPham).GetProperties();
            foreach (PropertyInfo prop in dto.GetType().GetProperties())
            {
                PropertyInfo propT = Array.Find(properties, p => p.Name == prop.Name);
                Type type = prop.PropertyType;
                if (prop != null && propT != null)
                {
                    object value = prop.GetValue(dto);
                    if (value != null)
                    {
                        propT.SetValue(existingEntity, value);
                    }
                }
            }
            db.SanPhams.AddOrUpdate(existingEntity);
            db.SaveChanges();
            return existingEntity.ToDTO();
        }
    }
}