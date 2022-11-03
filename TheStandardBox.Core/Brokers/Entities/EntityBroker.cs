// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Core.Brokers.Entities
{
    public class EntityBroker : IEntityBroker
    {
        public virtual string GetEntityName(Type type) => type.Name;

        public virtual string GetEntityName<TModel>() => typeof(TModel).Name;

        public virtual string GetRelativeApiUrl<TModel>(string prefix = "api")
            where TModel : IStandardEntity
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return $"{typeof(TModel).Name}s";

            return $"{prefix}/{typeof(TModel).Name}s";
        }
    }
}