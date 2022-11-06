// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Services.Foundations.Standards
{
    public interface IStandardService<TEntity>
        where TEntity : class, IStandardEntity
    {
        ValueTask<TEntity> AddEntityAsync(TEntity entity);
        IQueryable<TEntity> RetrieveAllEntities();
        ValueTask<TEntity> RetrieveEntityByIdAsync(Guid entityId);
        ValueTask<TEntity> RetrieveEntityByIdAsync(Guid entityId1, Guid entityId2);
        ValueTask<TEntity> ModifyEntityAsync(TEntity entity);
        ValueTask<TEntity> RemoveEntityByIdAsync(Guid entityId);
    }
}