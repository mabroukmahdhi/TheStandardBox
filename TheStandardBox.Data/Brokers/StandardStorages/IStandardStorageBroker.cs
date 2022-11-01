// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Brokers.StandardStorages
{
    public interface IStandardStorageBroker<TEntity>
        where TEntity : IStandardEntity
    {
        ValueTask<TEntity> InsertEntityAsync(TEntity entity);
        IQueryable<TEntity> SelectAllEntities();
        ValueTask<TEntity> SelectEntityByIdAsync(params object[] entityIds);
        ValueTask<TEntity> UpdateEntityAsync(TEntity entity);
        ValueTask<TEntity> DeleteEntityAsync(TEntity entity);
    }
}
