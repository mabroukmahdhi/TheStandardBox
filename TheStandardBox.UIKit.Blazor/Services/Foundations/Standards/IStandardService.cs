// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Services.Foundations.Standards
{
    public interface IStandardService<TEntity>
        where TEntity : IStandardEntity
    {
        ValueTask<TEntity> AddEntityAsync(TEntity entity);
        ValueTask<List<TEntity>> RetrieveAllEntitysAsync();
        ValueTask<TEntity> RetrieveEntityByIdAsync(Guid entityId);
        ValueTask<TEntity> ModifyEntityAsync(TEntity entity);
        ValueTask<TEntity> RemoveEntityByIdAsync(Guid entityId);
    }
}