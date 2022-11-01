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
        public static void AddTheStandardBoxData(this IServiceCollection services)
        {
            services.AddTheStandardBoxCore();
        }

        public static void AddStandardStorage<TEntity>(this IServiceCollection services)
                    where TEntity : class, IStandardEntity
        {
            services.AddScoped<IStandardStorageBroker<TEntity>, StandardStorageBroker<TEntity>>();
        }

        public static void AddStandardFoundationService<TEntity>(this IServiceCollection services)
                    where TEntity : class, IStandardEntity
        {
            services.AddScoped<IStandardService<TEntity>, StandardService<TEntity>>();
        }
    }
}
