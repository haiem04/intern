using demo.CRUD;
using demo.DTO;
using demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace demo.Business
{
    public class DotPhatHanhBusiness
    {
        private static internShipEntities db = new internShipEntities();
        public static DotPhatHanhDTO Create(JObject reqObjectBody)
        {
            DotPhatHanhDTO data = reqObjectBody.ToObject<DotPhatHanhDTO>();
            db.DotPhatHanhs.Add(data.ToModel());
            db.SaveChanges();
            return data;

        }

        public static DotPhatHanhDTO Update(JObject reqObjectBody)
        {
           /* var keyName = CRUDExtensions.GetPrimaryKeyName<SanPham>();
            PropertyInfo primaryKeyPropertyT = typeof(SanPham).GetProperty(keyName);
            JToken jToken = reqObjectBody[keyName];*/

            DotPhatHanhDTO dto = reqObjectBody.ToObject<DotPhatHanhDTO>();
            DotPhatHanh existingEntity = db.DotPhatHanhs.ToList().FirstOrDefault(x => x.idDPH == dto.idDPH);

            if (existingEntity == null)
            {
                return null;
            }
            PropertyInfo[] properties = typeof(DotPhatHanh).GetProperties();
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
            db.DotPhatHanhs.AddOrUpdate(existingEntity);
            db.SaveChanges();
            return existingEntity.ToDTO();
        }

        //public static 


    }
}