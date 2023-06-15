using demo.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace demo.CRUD
{
    public static class DeleteEntity
    {
        private static internShipEntities db = new internShipEntities();

        public static int Delete<T,TDto>(JArray ids) where T: class where TDto : class
        {
            //  check and ask comfrim delete relationship between them
            var keyName = CRUDExtensions.GetPrimaryKeyName<T>();
            PropertyInfo primaryKeyPropertyT = typeof(T).GetProperty(keyName);

            try
            {
                List<T> entites = new List<T>();
                foreach (var id in ids)
                {
                    T existingEntity = db.Set<T>().ToList().FirstOrDefault(x => primaryKeyPropertyT.GetValue(x).ToString() == id.ToString());
                    if (existingEntity != null)
                    {
                        entites.Add(existingEntity);
                    }
                }
                db.Set<T>().RemoveRange(entites);
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
        }
    }
}