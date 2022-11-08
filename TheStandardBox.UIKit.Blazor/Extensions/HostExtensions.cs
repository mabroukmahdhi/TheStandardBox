// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using TheStandardBox.Core.Brokers.Localizations;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Brokers.Apis;
using TheStandardBox.UIKit.Blazor.Brokers.LocalStorages;
using TheStandardBox.UIKit.Blazor.Services.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Extensions
{
    public static class HostExtensions
    {
        public static void AddTheStandardBox(this IServiceCollection services)
        {
            services.AddTheStandardBoxCore();
            services.AddBrokers();
        }

        public static void AddFoundationService<TEntity>(this IServiceCollection services)
                    where TEntity : class, IStandardEntity
        {
            services.AddScoped<IStandardService<TEntity>, StandardService<TEntity>>();
        }

        private static void AddBrokers(this IServiceCollection services)
        {
            services.AddScoped<IStandardApiBroker, StandardApiBroker>();
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddScoped<ILocalStorageBroker, LocalStorageBroker>();
            services.AddScoped<ILocalizationBroker, LocalizationBroker>();
        }
    }
}