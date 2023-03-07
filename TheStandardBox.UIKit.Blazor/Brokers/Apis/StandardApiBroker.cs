// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.WebAssembly.Clients;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Brokers.Apis
{
    public class StandardApiBroker : IStandardApiBroker
    {
        protected readonly IRESTFulApiFactoryClient apiClient;
        protected readonly HttpClient httpClient;

        public StandardApiBroker(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.apiClient = new RESTFulApiFactoryClient(this.httpClient);
        }

        public virtual async ValueTask<List<TEntity>> GetAllEntitiesAsync<TEntity>(string relativeUrl)
            where TEntity : IStandardEntity =>
                await this.apiClient.GetContentAsync<List<TEntity>>(relativeUrl);

        public virtual async ValueTask<TEntity> GetEntityByIdAsync<TEntity>(Guid modelId, string relativeUrl)
            where TEntity : IStandardEntity =>
                await this.apiClient.GetContentAsync<TEntity>(relativeUrl);

        public virtual async ValueTask<TEntity> PostEntityAsync<TEntity>(TEntity model, string relativeUrl)
            where TEntity : IStandardEntity =>
                await this.apiClient.PostContentAsync(relativeUrl, model);

        public virtual async ValueTask<TEntity> PutEntityAsync<TEntity>(TEntity model, string relativeUrl)
            where TEntity : IStandardEntity =>
                await this.apiClient.PutContentAsync(relativeUrl, model);

        public virtual async ValueTask<TEntity> DeleteEntityByIdAsync<TEntity>(Guid modelId, string relativeUrl)
            where TEntity : IStandardEntity =>
                await this.apiClient.DeleteContentAsync<TEntity>(relativeUrl);
    }
}