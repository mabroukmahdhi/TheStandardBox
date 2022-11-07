// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Brokers.Apis
{
    public interface IStandardApiBroker
    {
        ValueTask<TEntity> PostEntityAsync<TEntity>(TEntity model, string relativeUrl)
            where TEntity : IStandardEntity;

        ValueTask<List<TEntity>> GetAllEntitiesAsync<TEntity>(string relativeUrl)
            where TEntity : IStandardEntity;

        ValueTask<TEntity> GetEntityByIdAsync<TEntity>(Guid modelId, string relativeUrl)
            where TEntity : IStandardEntity;

        ValueTask<TEntity> PutEntityAsync<TEntity>(TEntity model, string relativeUrl)
            where TEntity : IStandardEntity;

        ValueTask<TEntity> DeleteEntityByIdAsync<TEntity>(Guid modelId, string relativeUrl)
            where TEntity : IStandardEntity;
    }
}