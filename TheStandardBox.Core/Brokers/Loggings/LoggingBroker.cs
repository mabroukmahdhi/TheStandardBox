// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;

namespace TheStandardBox.Core.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        protected readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public virtual void LogInformation(string message) =>
            this.logger.LogInformation(message);

        public virtual void LogTrace(string message) =>
            this.logger.LogTrace(message);

        public virtual void LogDebug(string message) =>
            this.logger.LogDebug(message);

        public virtual void LogWarning(string message) =>
            this.logger.LogWarning(message);

        public virtual void LogError(Exception exception) =>
            this.logger.LogError(exception.Message, exception);

        public virtual void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);
    }
}