// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using TheStandardBox.Core.Brokers.DateTimes;
using TheStandardBox.Core.Brokers.Entities;
using TheStandardBox.Core.Brokers.Loggings;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Data.Brokers.StandardStorages;
using TheStandardBox.Data.Services.Foundations.Standards;

namespace TheStandardBox.Data.Services.Standards
{
    public partial class StandardService<TEntity> : IStandardService<TEntity>
        where TEntity : class, IStandardEntity
    {
        protected readonly IStandardStorageBroker standardStorageBroker;
        protected readonly IDateTimeBroker dateTimeBroker;
        protected readonly ILoggingBroker loggingBroker;
        protected readonly string entityName;

        public StandardService(
            IStandardStorageBroker standardStorageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker,
            IStandardEntityBroker entityBroker)
        {
            this.standardStorageBroker = standardStorageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;

            this.entityName = entityBroker.GetEntityName<TEntity>();
        }

        public virtual ValueTask<TEntity> AddEntityAsync(TEntity entity) =>
            TryCatch(async () =>
            {
                ValidateEntityOnAdd(entity);

                return await this.standardStorageBroker.InsertEntityAsync(entity);
            });

        public virtual IQueryable<TEntity> RetrieveAllEntities() =>
            TryCatch(() => this.standardStorageBroker.SelectAllEntities<TEntity>());

        public virtual ValueTask<TEntity> RetrieveEntityByIdAsync(Guid entityId) =>
            TryCatch(async () =>
            {
                ValidateEntityId(entityId);

                TEntity maybeTEntity = await this.standardStorageBroker
                    .SelectEntityByIdAsync<TEntity>(entityId);

                ValidateStorageEntity(maybeTEntity, entityId);

                return maybeTEntity;
            });

        public ValueTask<TEntity> RetrieveEntityByIdAsync(
            Guid entityId1,
            Guid entityId2) =>
        TryCatch(async () =>
            {
                ValidateEntityId(entityId1);
                ValidateEntityId(entityId2);

                TEntity maybeTEntity = await this.standardStorageBroker
                    .SelectEntityByIdAsync<TEntity>(entityId1, entityId2);

                ValidateStorageEntity(maybeTEntity, entityId1, entityId2);

                return maybeTEntity;
            });

        public virtual ValueTask<TEntity> ModifyEntityAsync(TEntity entity) =>
            TryCatch(async () =>
            {
                ValidateEntityOnModify(entity);

                TEntity maybeTEntity =
                    await this.standardStorageBroker.SelectEntityByIdAsync<TEntity>(entity.GetPrimaryKeys());

                ValidateStorageEntity(maybeTEntity, entity.GetPrimaryKeys());
                ValidateAgainstStorageEntityOnModify(inputEntity: entity, storageEntity: maybeTEntity);

                return await this.standardStorageBroker.UpdateEntityAsync(entity);
            });

        public virtual ValueTask<TEntity> RemoveEntityByIdAsync(Guid entityId) =>
            TryCatch(async () =>
            {
                ValidateEntityId(entityId);

                TEntity maybeTEntity = await this.standardStorageBroker
                    .SelectEntityByIdAsync<TEntity>(entityId);

                ValidateStorageEntity(maybeTEntity, entityId);

                return await this.standardStorageBroker.DeleteEntityAsync(maybeTEntity);
            });
    }
}
