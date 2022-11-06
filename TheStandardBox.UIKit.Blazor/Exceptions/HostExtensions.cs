// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using TheStandardBox.Core.Extensions;
using TheStandardBox.UIKit.Blazor.Brokers.Apis;
using TheStandardBox.UIKit.Blazor.Brokers.Localizations;
using TheStandardBox.UIKit.Blazor.Brokers.LocalStorages;

namespace TheStandardBox.UIKit.Blazor.Exceptions
{
    public static class HostExtensions
    {
        public static void AddTheStandardBox(this IServiceCollection services)
        {
            services.AddTheStandardBoxCore();
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
