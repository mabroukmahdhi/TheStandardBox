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
        ValueTask<TModel> PostModelAsync<TModel>(TModel model, string relativeUrl)
            where TModel : IStandardEntity;

        ValueTask<List<TModel>> GetAllModelsAsync<TModel>(string relativeUrl)
            where TModel : IStandardEntity;

        ValueTask<TModel> GetModelByIdAsync<TModel>(Guid modelId, string relativeUrl)
            where TModel : IStandardEntity;

        ValueTask<TModel> PutModelAsync<TModel>(TModel model, string relativeUrl)
            where TModel : IStandardEntity;

        ValueTask<TModel> DeleteModelByIdAsync<TModel>(Guid modelId, string relativeUrl)
            where TModel : IStandardEntity;
    }
}