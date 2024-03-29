﻿// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Bases;

namespace TheStandardBox.Data.Brokers.StandardStorages
{
    public interface IStandardStorageBroker
    {
        ValueTask<TEntity> InsertEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        IQueryable<TEntity> SelectAllEntities<TEntity>()
            where TEntity : class, IBaseEntity;

        ValueTask<TEntity> SelectEntityByIdAsync<TEntity>(Guid entityId)
            where TEntity : class, IBaseEntity;

        ValueTask<TEntity> SelectEntityByIdAsync<TEntity>(params object[] entityIds)
            where TEntity : class, IBaseEntity;

        ValueTask<TEntity> UpdateEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        ValueTask<TEntity> DeleteEntityAsync<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;
    }
}