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

namespace demo.Business
{
    public class ProductBusiness
    {
        private static internShipEntities db = new internShipEntities();

        public static dynamic FilterProduct(JObject requestBodyObject)
        {
            JObject filterQuery = requestBodyObject["Filter"] as JObject;

            List<SanPhamDTO> filteredItems = ProductBusiness.Filter(filterQuery);

            int pageNumber = requestBodyObject?["currentPage"]?.ToObject<int>() ?? 1;
            int pageSize = requestBodyObject?["pageSize"]?.ToObject<int>() ?? 5;
            int totalItems = filteredItems.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            filteredItems = filteredItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
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

        public static List<SanPhamDTO> Filter(JObject filterQuery)
        {
            internShipEntities db = new internShipEntities();
            List<SanPhamDTO> filteredItems = null;
            if (filterQuery == null)
            {
                filteredItems = db.SanPhams.ToList().Select(x => x.ToDTO()).ToList();
            }
            else
            {
                var keyName = CRUDExtensions.GetPrimaryKeyName<SanPham>();
                PropertyInfo primaryKeyPropertyT = typeof(SanPham).GetProperty(keyName);
                JToken jToken = filterQuery[keyName];
                var prop = filterQuery.Properties();
                if (jToken != null)
                {
                    int[] ids = jToken is JArray array ? ids = array.Select(t => (int)t).ToArray() : ids = new int[] { (int)jToken };

                    filteredItems = db.SanPhams.ToList().Where(x => ids.Contains((int)primaryKeyPropertyT.GetValue(x))).Select(x => x.ToDTO()).ToList();
                }
                else
                {
                    List<SanPham> filtered = new List<SanPham>();
                    List<SanPham> filterList = db.SanPhams.ToList();
                    foreach (var property in filterQuery.Properties())
                    {
                        filtered.Clear();
                        JTokenType typeOfToken = property.Value.Type;
                        filtered.AddRange(objectConditon(property, filterList));
                        filterList.Clear();
                        filterList.AddRange(filtered);
                    }
                    filteredItems = filtered.Select(x => x.ToDTO()).ToList();
                }
            }
            

          
            return filteredItems;
        }


        public static List<SanPham> objectConditon(JProperty prop, List<SanPham> filterList)
        {
            var propName = typeof(SanPham).GetProperty(prop.Name);
            JToken token = prop.Value;
            JObject obj = token as JObject;
            JArray arr = token as JArray;

            List<SanPham> filtered = new List<SanPham>();
            if (obj != null)
            {
                foreach (var item in obj.Properties())
                {
                    filtered.Clear();
                    switch (item.Name)
                    {

                        case "eq":
                            filtered.AddRange(filterList.Where(x => (double)propName.GetValue(x) == (double)item.Value));
                            // conditions.Add($"{propName} = \"{property.Value}\"");
                            break;
                        case "lt":
                            filtered.AddRange(filterList.Where(x => (double)propName.GetValue(x) < (double)item.Value));
                            break;
                        case "lte":
                            filtered.AddRange(filterList.Where(x => (double)propName.GetValue(x) <= (double)item.Value));

                            break;
                        case "gt":
                            filtered.AddRange(filterList.Where(x => (double)propName.GetValue(x) > (double)item.Value));

                            break;
                        case "gte":
                            filtered.AddRange(filterList.Where(x => (double)propName.GetValue(x) >= (double)item.Value));

                            break;
                        default:
                            break;
                    }
                    filterList.Clear();
                    filterList.AddRange(filtered);
                }
            }
            else
            {
                string[] arrCondition = arr is JArray ? arrCondition = arr.Select(t => (string)t).ToArray() : arrCondition = new string[] { (string)token };
                filtered.AddRange(filterList.Where(x => arrCondition.Contains(propName.GetValue(x).ToString())).ToList());
            }
            return filtered;
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

        public static List<SanPhamDTO> UpdateProduct(JObject data)
        {
            var keyName = CRUDExtensions.GetPrimaryKeyName<SanPham>();
            PropertyInfo primaryKeyPropertyT = typeof(SanPham).GetProperty(keyName);
            JToken jToken = data[keyName];
            int[] ids = jToken is JArray array ? ids = array.Select(t => (int)t).ToArray() : ids = new int[] { (int)jToken };
            if (ids.Length > 0)
            {
                data.Remove(keyName);
                return UpdateMultiProduct(data, ids, primaryKeyPropertyT, keyName);
            }
            SanPhamDTO dto = data.ToObject<SanPhamDTO>();
            SanPham existingEntity = db.SanPhams.ToList().FirstOrDefault(x => primaryKeyPropertyT.GetValue(x).ToString() == ids[0].ToString());

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
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            list.Add(existingEntity.ToDTO());
            return list;
        }

        public static dynamic UpdateMultiProduct(JObject data, int[] ids, PropertyInfo primaryKeyPropertyT, string keyName)
        {
            SanPhamDTO dto = data.ToObject<SanPhamDTO>();
            List<SanPham> listSanPham = db.SanPhams.ToList();
            List<SanPhamDTO> listDTOs = new List<SanPhamDTO>();
            listSanPham = listSanPham.Where(x => ids.Contains((int)primaryKeyPropertyT.GetValue(x))).ToList();
            PropertyInfo[] properties = typeof(SanPham).GetProperties();
            foreach (SanPham item in listSanPham)
            {
                foreach (PropertyInfo prop in dto.GetType().GetProperties())
                {
                    PropertyInfo propT = Array.Find(properties, p => p.Name == prop.Name);
                    Type type = prop.PropertyType;
                    if (prop != null && propT != null && prop.Name != keyName)
                    {
                        object value = prop.GetValue(dto);
                        if (value != null)
                        {
                            propT.SetValue(item, value);
                        }
                    }
                }
                Debug.WriteLine(item.maSP);
                listDTOs.Add(item.ToDTO());
                db.SanPhams.AddOrUpdate(item);

            }
            db.SaveChanges();
            return listDTOs;
        }
    }
}