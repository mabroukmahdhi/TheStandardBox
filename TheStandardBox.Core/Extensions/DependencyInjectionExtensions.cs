// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheStandardBox.Core.Brokers.DateTimes;
using TheStandardBox.Core.Brokers.Entities;
using TheStandardBox.Core.Brokers.Loggings;

namespace TheStandardBox.Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddTheStandardBoxCore(this IServiceCollection services)
        {
            services.AddTransient<ILogger, Logger<LoggingBroker>>();
            services.AddSingleton<ILoggingBroker, LoggingBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddTransient<IStandardEntityBroker, StandardEntityBroker>();
        }
    }
}