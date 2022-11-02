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
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Data.Brokers.StandardStorages;
using TheStandardBox.Data.Services.Foundations.Standards;

namespace TheStandardBox.Data.Services.Standards
{
    public partial class StandardService<TEntity> : IStandardService<TEntity>
        where TEntity : class, IStandardEntity
    {
        protected readonly IStandardStorageBroker<TEntity> standardStorageBroker;
        protected readonly IDateTimeBroker dateTimeBroker;
        protected readonly ILoggingBroker loggingBroker;
        protected readonly string entityName;

        public StandardService(
            IStandardStorageBroker<TEntity> standardStorageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker,
            IEntityBroker entityBroker)
        {
            this.standardStorageBroker = standardStorageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;

            this.entityName = entityBroker.GetEntityName<TEntity>();
        }

        public ValueTask<TEntity> AddEntityAsync(TEntity model) =>
            TryCatch(async () =>
            {
                ValidateEntityOnAdd(model);

                return await this.standardStorageBroker.InsertEntityAsync(model);
            });

        public IQueryable<TEntity> RetrieveAllEntities() =>
            TryCatch(() => this.standardStorageBroker.SelectAllEntities());

        public ValueTask<TEntity> RetrieveEntityByIdAsync(Guid modelId) =>
            TryCatch(async () =>
            {
                ValidateEntityId(modelId);

                TEntity maybeTEntity = await this.standardStorageBroker
                    .SelectEntityByIdAsync(modelId);

                ValidateStorageEntity(maybeTEntity, modelId);

                return maybeTEntity;
            });

        public ValueTask<TEntity> ModifyEntityAsync(TEntity model) =>
            TryCatch(async () =>
            {
                ValidateEntityOnModify(model);

                TEntity maybeTEntity =
                    await this.standardStorageBroker.SelectEntityByIdAsync(model.Id);

                ValidateStorageEntity(maybeTEntity, model.Id);
                ValidateAgainstStorageEntityOnModify(inputEntity: model, storageEntity: maybeTEntity);

                return await this.standardStorageBroker.UpdateEntityAsync(model);
            });

        public ValueTask<TEntity> RemoveEntityByIdAsync(Guid modelId) =>
            TryCatch(async () =>
            {
                ValidateEntityId(modelId);

                TEntity maybeTEntity = await this.standardStorageBroker
                    .SelectEntityByIdAsync(modelId);

                ValidateStorageEntity(maybeTEntity, modelId);

                return await this.standardStorageBroker.DeleteEntityAsync(maybeTEntity);
            });
    }
}
