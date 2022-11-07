// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStandardBox.Core.Brokers.Entities;
using TheStandardBox.Core.Brokers.Loggings;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Brokers.Apis;

namespace TheStandardBox.UIKit.Blazor.Services.Foundations.Standards
{
    public partial class StandardService<TEntity> : IStandardService<TEntity>
         where TEntity : IStandardEntity
    {
        private readonly IStandardApiBroker apiStandardBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IStandardEntityBroker entityBroker;
        private readonly string relativeUrl;
        private readonly string entityName;

        public StandardService(
            IStandardApiBroker apiStandardBroker,
            ILoggingBroker loggingBroker,
            IStandardEntityBroker entityBroker)
        {
            this.apiStandardBroker = apiStandardBroker;
            this.loggingBroker = loggingBroker;
            this.entityBroker = entityBroker;

            this.relativeUrl =
                this.entityBroker.GetRelativeApiUrl<TEntity>();
            this.entityName = this.entityBroker.GetEntityName<TEntity>();
        }

        public ValueTask<TEntity> AddEntityAsync(TEntity model) =>
        TryCatch(async () =>
        {
            ValidateEntityOnAdd(model);

            return await this.apiStandardBroker.PostEntityAsync(model, this.relativeUrl);
        });

        public ValueTask<List<TEntity>> RetrieveAllEntitiesAsync() =>
        TryCatch(async () =>
        {
            return await this.apiStandardBroker.GetAllEntitiesAsync<TEntity>(this.relativeUrl);
        });

        public ValueTask<TEntity> RetrieveEntityByIdAsync(Guid modelId) =>
        TryCatch(async () =>
        {
            ValidateEntityId(modelId);

            return await this.apiStandardBroker.GetEntityByIdAsync<TEntity>(
                modelId: modelId,
                relativeUrl: this.relativeUrl);
        });

        public ValueTask<TEntity> ModifyEntityAsync(TEntity model) =>
        TryCatch(async () =>
        {
            ValidateEntityOnUpdate(model);

            return await this.apiStandardBroker.PutEntityAsync(model, this.relativeUrl);
        });

        public ValueTask<TEntity> RemoveEntityByIdAsync(Guid modelId) =>
        TryCatch(async () =>
        {
            ValidateEntityId(modelId);

            return await this.apiStandardBroker.DeleteEntityByIdAsync<TEntity>(
                modelId: modelId,
                relativeUrl: this.relativeUrl);
        });
    }
}
