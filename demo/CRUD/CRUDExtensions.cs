using demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace demo.CRUD
{
    public static class CRUDExtensions
    {
        public static TDto MapToDto<T, TDto>(T entity) where T : class where TDto : class
        {
            var dto = Activator.CreateInstance<TDto>();
            var entityType = typeof(T);
            var dtoType = typeof(TDto);
            var entityProperties = entityType.GetProperties();
            var dtoProperties = dtoType.GetProperties();

            foreach (var dtoProp in dtoProperties)
            {
                var matchingProp = entityProperties.FirstOrDefault(prop => prop.Name == dtoProp.Name);
                if (matchingProp != null && matchingProp.CanRead)
                {
                    var value = matchingProp.GetValue(entity);
                    dtoProp.SetValue(dto, value);
                }
            }

            return dto as TDto;
        }


        public static string GetPrimaryKeyName<T>() where T : class
        {
            using (var dbContext = new internShipEntities())
            {
                ObjectContext objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                ObjectSet<T> objectSet = objectContext.CreateObjectSet<T>();
                IEnumerable<string> keyNames = objectSet.EntitySet.ElementType
                    .KeyMembers
                    .Select(k => k.Name);

                return keyNames.FirstOrDefault();
            }
        }
    }
}