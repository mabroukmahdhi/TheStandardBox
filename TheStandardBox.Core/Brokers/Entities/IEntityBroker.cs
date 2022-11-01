// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Core.Brokers.Entities
{
    public interface IEntityBroker
    {
        string GetEntityName(Type type);
        string GetEntityName<TModel>();

        string GetRelativeApiUrl<TModel>()
           where TModel : IStandardEntity;
    }
}
