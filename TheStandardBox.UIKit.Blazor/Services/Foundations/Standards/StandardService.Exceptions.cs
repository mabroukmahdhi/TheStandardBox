// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RESTFulSense.WebAssembly.Exceptions;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using TheStandardBox.UIKit.Blazor.Models.Foundations.Standards;
using Xeptions;

namespace TheStandardBox.UIKit.Blazor.Services.Foundations.Standards
{
    public partial class StandardService<TEntity>
    {
        protected delegate ValueTask<TEntity> ReturningEntityFunction();
        protected delegate ValueTask<List<TEntity>> ReturningEntitiesFunction();

        protected async ValueTask<TEntity> TryCatch(ReturningEntityFunction returningEntityFunction)
        {
            try
            {
                return await returningEntityFunction();
            }
            catch (NullEntityException nullEntityException)
            {
                throw CreateAndLogValidationException(nullEntityException);
            }
            catch (InvalidEntityException invalidEntityException)
            {
                throw CreateAndLogValidationException(invalidEntityException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedEntityDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedEntityDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedEntityDependencyException);
            }
            catch (HttpResponseNotFoundException httpResponseNotFoundException)
            {
                var notFoundEntityException =
                    new NotFoundEntityException(this.entityName, httpResponseNotFoundException);

                throw CreateAndLogDependencyValidationException(notFoundEntityException);
            }
            catch (HttpResponseBadRequestException httpResponseBadRequestException)
            {
                var invalidEntityException =
                    new InvalidEntityException(
                        this.entityName,
                        httpResponseBadRequestException,
                        httpResponseBadRequestException.Data);

                throw CreateAndLogDependencyValidationException(invalidEntityException);
            }
            catch (HttpResponseConflictException httpResponseConflictException)
            {
                var invalidEntityException =
                    new InvalidEntityException(
                        this.entityName,
                        httpResponseConflictException,
                        httpResponseConflictException.Data);

                throw CreateAndLogDependencyValidationException(invalidEntityException);
            }
            catch (HttpResponseLockedException httpLockedException)
            {
                var lockedEntityException =
                    new LockedEntityException(this.entityName, httpLockedException);

                throw CreateAndLogDependencyValidationException(lockedEntityException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpResponseException);

                throw CreateAndLogDependencyException(failedEntityDependencyException);
            }
            catch (Exception exception)
            {
                var failedEntityServiceException =
                    new FailedEntityServiceException(this.entityName, exception);

                throw CreateAndLogEntityServiceException(failedEntityServiceException);
            }
        }

        protected async ValueTask<List<TEntity>> TryCatch(ReturningEntitiesFunction returningEntitiesFunction)
        {
            try
            {
                return await returningEntitiesFunction();
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpRequestException);

                throw CreateAndLogCriticalDependencyException(failedEntityDependencyException);
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedEntityDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedEntityDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedEntityDependencyException =
                    new FailedEntityDependencyException(this.entityName, httpResponseException);

                throw CreateAndLogDependencyException(failedEntityDependencyException);
            }
            catch (Exception exception)
            {
                var failedEntityServiceException =
                    new FailedEntityServiceException(this.entityName, exception);

                throw CreateAndLogEntityServiceException(failedEntityServiceException);
            }
        }

        protected EntityValidationException CreateAndLogValidationException(Xeption exception)
        {
            var commentValidationException =
                new EntityValidationException(this.entityName, exception);

            this.loggingBroker.LogError(commentValidationException);

            return commentValidationException;
        }

        protected EntityDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new EntityDependencyException(this.entityName, exception);

            this.loggingBroker.LogCritical(commentDependencyException);

            return commentDependencyException;
        }

        protected EntityDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var commentDependencyValidationException =
                new EntityDependencyValidationException(this.entityName, exception);

            this.loggingBroker.LogError(commentDependencyValidationException);

            return commentDependencyValidationException;
        }

        protected EntityDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var commentDependencyException =
                new EntityDependencyException(this.entityName, exception);

            this.loggingBroker.LogError(commentDependencyException);

            return commentDependencyException;
        }

        protected EntityServiceException CreateAndLogEntityServiceException(Xeption exception)
        {
            var commentServiceException =
                new EntityServiceException(this.entityName, exception);

            this.loggingBroker.LogError(commentServiceException);

            return commentServiceException;
        }
    }
}