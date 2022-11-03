// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Data.Brokers.StandardStorages;
using TheStandardBox.Data.Services.Foundations.Standards;
using TheStandardBox.Data.Services.Standards;

namespace TheStandardBox.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTheStandardBoxData(this IServiceCollection services)
        {
            return services.AddTheStandardBoxData<StandardStorageBroker>();
        }

        public static IServiceCollection AddTheStandardBoxData<TDbContext>(this IServiceCollection services)
            where TDbContext : StandardStorageBroker
        {
            services.AddTheStandardBoxCore();
            services.AddDbContext<TDbContext>();
            services.AddScoped<IStandardStorageBroker, StandardStorageBroker>();
            return services;
        }

        public static IServiceCollection AddStandardFoundationService<TEntity>(this IServiceCollection services)
                    where TEntity : class, IStandardEntity
        {
            services.AddScoped<IStandardService<TEntity>, StandardService<TEntity>>();
            return services;
        }
    }
}