// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheStandardBox.Core.Attributes.Annotations;
using TheStandardBox.Core.Brokers.Entities;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;

namespace TheStandardBox.Core.Extensions
{
    public static class EntityExtensions
    {
        private static readonly IStandardEntityBroker standardEntityBroker = new StandardEntityBroker();

        public static object[] GetPrimaryKeys<TEntity>(this TEntity entity)
        {
            var properties = GetPrimaryKeyProperties(entity);

            List<object> ids = new();

            foreach (var property in properties)
            {
                ids.Add(property.GetValue(entity));
            }

            return ids.ToArray();
        }

        public static TIdentifier GetPrimaryKey<TEntity, TIdentifier>(this TEntity entity)
        {
            var properties = GetPrimaryKeyProperties(entity);

            List<object> ids = new();

            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(TIdentifier))
                    continue;

                ids.Add(property.GetValue(entity));
            }

            return (TIdentifier)ids?.FirstOrDefault();
        }

        public static string GetPrimaryKeyName<TEntity>(this TEntity entity)
        {
            var properties = GetPrimaryKeyProperties(entity);

            List<string> names = new();

            foreach (var property in properties)
            {
                names.Add(property.Name);
            }

            return string.Join("_", names);
        }

        public static IEnumerable<PropertyInfo> GetPrimaryKeyProperties<TEntity>(this TEntity entity)
        {
            var properties = entity.GetType().GetProperties().Where(p =>
                Attribute.IsDefined(p, typeof(PrimaryKeyAttribute)));

            if (properties == null || properties.Any())
            {
                var entityName = standardEntityBroker.GetEntityName<TEntity>();

                var invalidEntityException = new InvalidEntityException(entityName);

                invalidEntityException.AddData("PrimaryKey", "No key was defined");

                throw new EntityValidationException(entityName, invalidEntityException);
            }

            return properties;
        }
    }
}
