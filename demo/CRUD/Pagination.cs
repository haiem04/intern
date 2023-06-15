using demo.CRUD;
using demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Web;

namespace demo.DTO
{
    public class Pagination
    {
        public int pageSize { get; set; } 
        public int currentPage { get; set; } 
        public int? totalItem { get; set; }
        public int? totalPages { get; set; }
        public List<object> filteredItems { get; set; } = null;
        public dynamic filterQuery { get; set; }
        public static Pagination Filter<T, TDto>(JObject filterQuery) where T : class where TDto : class
        {
            internShipEntities db = new internShipEntities();
            IQueryable<T> query = db.Set<T>().AsQueryable();
            List<TDto> filteredItems = null;

            if (filterQuery == null)
            {
                filteredItems = query.ToList().Select(x => CRUDExtensions.MapToDto<T, TDto>(x)).ToList();
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
                        break;
                    }
                    else if (typeOfToken == JTokenType.Array)
                    {
                        List<string> propertyValues = token.ToObject<List<string>>(); 
                        condition = $"{propName} IN ({string.Join(",", propertyValues)})";
                        break;

                    }
                    else
                    {
                        condition = $"{propName} = \"{property.Value}\"";
                        break;
                    }

                   
                }

                string combinedConditions = string.Join(" AND ", conditions);
                query = query.Where(combinedConditions);
                filteredItems = query.ToList().Select(x => CRUDExtensions.MapToDto<T, TDto>(x)).ToList();
            }

            int pageNumber = filterQuery?["currentPage"]?.ToObject<int>() ?? 1;
            int pageSize = filterQuery?["pageSize"]?.ToObject<int>() ?? 5;
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            Pagination result = new Pagination
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

       
        public static string objectConditon(string propName,JToken token)
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
        
    }
}