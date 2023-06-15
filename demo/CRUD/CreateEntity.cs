using demo.DTO;
using demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace demo.CRUD
{
    public static class CreateEntity
    {
        private static internShipEntities db = new internShipEntities();
        public static TDto Create<T,TDto>(JObject data) where T: class where TDto : class
        {

            TDto dto = data.ToObject<TDto>();
            T entity = Activator.CreateInstance<T>();
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
                        propT.SetValue(entity, value);
                    }
                }
            }

            db.Set<T>().Add(entity);
            db.SaveChanges();
            return CRUDExtensions.MapToDto<T,TDto>(entity);
        }
    }
}