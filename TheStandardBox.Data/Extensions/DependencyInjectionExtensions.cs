// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Bases;
using TheStandardBox.Data.Brokers.StandardStorages;
using TheStandardBox.Data.Controllers.Conventions;
using TheStandardBox.Data.Controllers.Providers;
using TheStandardBox.Data.Services.Foundations.Standards;
using TheStandardBox.Data.Services.Standards;

namespace TheStandardBox.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTheStandardBoxData(
            this IServiceCollection services,
            Action<MvcOptions> configure = null)
        {
            return services.AddTheStandardBoxData<StandardStorageBroker>();
        }

        public static IServiceCollection AddTheStandardBoxData<TDbContext>(
            this IServiceCollection services,
            Action<MvcOptions> configure = null)
            where TDbContext : StandardStorageBroker
        {
            services.AddTheStandardBoxCore();

            if (configure == null)
            {
                services.AddControllers(options =>
                {
                    options.Conventions.Add(new GenericControllerRouteConvention());
                    options.Conventions.Add(new AuthorizeControllerConvention());
                    options.Conventions.Add(new ActionAuthorizingConvention());
                    options.Conventions.Add(new ActionBuildingConvention());
                }).ConfigureApplicationPartManager(m =>
                {
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider());
                });
            }

            services.AddScoped<IStandardStorageBroker, StandardStorageBroker>();
            services.AddScoped<IStandardStorageBroker, TDbContext>();
            return services;
        }

        public static IServiceCollection AddStandardFoundationService<TEntity>(this IServiceCollection services)
                    where TEntity : class, IBaseEntity
        {
            services.AddScoped<IStandardService<TEntity>, StandardService<TEntity>>();
            return services;
        }
    }
}