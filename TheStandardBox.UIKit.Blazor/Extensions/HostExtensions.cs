// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TheStandardBox.Core.Brokers.Localizations;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Brokers.Apis;
using TheStandardBox.UIKit.Blazor.Brokers.Localizations;
using TheStandardBox.UIKit.Blazor.Models.Configurations;
using TheStandardBox.UIKit.Blazor.Services.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Services.Views.Renderings;
using TheStandardBox.UIKit.Blazor.Services.Views.StandardEdits;

namespace TheStandardBox.UIKit.Blazor.Extensions
{
    public static class HostExtensions
    {
        public static void AddTheStandardBox(this IServiceCollection services)
        {
            services.AddTheStandardBoxCore();
            services.AddStandardBrokers();
            services.AddStandardComponentRenderer<StandardRenderingService>();
        }

        public static void AddFoundationService<TEntity>(this IServiceCollection services)
            where TEntity : class, IStandardEntity
        {
            services.AddScoped<IStandardService<TEntity>, StandardService<TEntity>>();
        }

        public static void AddStandardEditViewService<TEntity>(this IServiceCollection services)
            where TEntity : class, IStandardEntity
        {
            services.AddScoped<IStandardEditViewService<TEntity>, StandardEditViewService<TEntity>>();
        }

        public static void AddStandardComponentRenderer<TRenderer>(this IServiceCollection services)
            where TRenderer : class, IStandardRenderingService
        {
            services.Replace(ServiceDescriptor.Scoped<IStandardRenderingService, TRenderer>());
        }

        public static void AddStandardHttpClient(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            LocalConfigurations localConfigurations =
                configuration.Get<LocalConfigurations>();

            Uri apiUri = new(localConfigurations.ApiConfigurations.Url);

            services.TryAddScoped(sp => new HttpClient
            {
                BaseAddress = apiUri
            });
        }

        private static void AddStandardBrokers(this IServiceCollection services)
        {
            services.AddScoped<IStandardApiBroker, StandardApiBroker>();
            services.Replace(ServiceDescriptor.Singleton<ILocalizationBroker, UILocalizationBroker>());
        }
    }
}