using demo.CRUD;
using demo.DTO;
using demo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace demo.CRUD
{
    public static class UpdateEntity
    {


        private static internShipEntities db = new internShipEntities();
        public static TDto Update<T,TDto>(JObject obj) where T : class where TDto : class
        {
            var keyName = CRUDExtensions.GetPrimaryKeyName<T>();
            TDto dto = obj.ToObject<TDto>();
            PropertyInfo primaryKeyPropertyT = typeof(T).GetProperty(keyName);
            PropertyInfo primaryKeyPropertyTDto = typeof(TDto).GetProperty(keyName);
            object primaryKeyValueTDto = primaryKeyPropertyTDto.GetValue(dto);
            T existingEntity = db.Set<T>().ToList().FirstOrDefault(x => primaryKeyPropertyT.GetValue(x).ToString()==primaryKeyValueTDto.ToString());
            
            if (existingEntity == null)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(T).GetProperties();
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
       

            db.Set<T>().AddOrUpdate(existingEntity);
            db.SaveChanges();
            return CRUDExtensions.MapToDto<T,TDto>(existingEntity);

        }

        
    }
}