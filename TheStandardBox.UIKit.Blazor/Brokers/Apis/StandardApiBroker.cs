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

        public virtual async ValueTask<List<TModel>> GetAllModelsAsync<TModel>(string relativeUrl)
            where TModel : IStandardEntity =>
                await this.apiClient.GetContentAsync<List<TModel>>(relativeUrl);

        public virtual async ValueTask<TModel> GetModelByIdAsync<TModel>(Guid modelId, string relativeUrl)
            where TModel : IStandardEntity =>
                await this.apiClient.GetContentAsync<TModel>(relativeUrl);

        public virtual async ValueTask<TModel> PostModelAsync<TModel>(TModel model, string relativeUrl)
            where TModel : IStandardEntity =>
                await this.apiClient.PostContentAsync(relativeUrl, model);

        public virtual async ValueTask<TModel> PutModelAsync<TModel>(TModel model, string relativeUrl)
            where TModel : IStandardEntity =>
                await this.apiClient.PutContentAsync(relativeUrl, model);

        public virtual async ValueTask<TModel> DeleteModelByIdAsync<TModel>(Guid modelId, string relativeUrl)
            where TModel : IStandardEntity =>
                await this.apiClient.DeleteContentAsync<TModel>(relativeUrl);
    }
}