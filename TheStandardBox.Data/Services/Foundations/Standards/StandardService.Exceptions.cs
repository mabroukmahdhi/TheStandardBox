// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using Xeptions;

namespace TheStandardBox.Data.Services.Standards
{
    public partial class StandardService<TEntity>
    {
        protected delegate ValueTask<TEntity> ReturningEntityFunction();
        protected delegate IQueryable<TEntity> ReturningEntitiesFunction();

        protected virtual async ValueTask<TEntity> TryCatch(ReturningEntityFunction returningEntityFunction)
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
            catch (SqlException sqlException)
            {
                var failedEntityStorageException =
                    new FailedEntityStorageException(this.entityName, sqlException);

                throw CreateAndLogCriticalDependencyException(failedEntityStorageException);
            }
            catch (NotFoundEntityException notFoundEntityException)
            {
                throw CreateAndLogValidationException(notFoundEntityException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsEntityException =
                    new AlreadyExistsEntityException(this.entityName, duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsEntityException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidEntityReferenceException =
                    new InvalidEntityReferenceException(this.entityName, foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(invalidEntityReferenceException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedEntityException = new LockedEntityException(this.entityName, dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedEntityException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedEntityStorageException =
                    new FailedEntityStorageException(this.entityName, databaseUpdateException);

                throw CreateAndLogDependencyException(failedEntityStorageException);
            }
            catch (Exception exception)
            {
                var failedEntityServiceException =
                    new FailedEntityServiceException(this.entityName, exception);

                throw CreateAndLogServiceException(failedEntityServiceException);
            }
        }

        protected virtual IQueryable<TEntity> TryCatch(ReturningEntitiesFunction returningEntitiesFunction)
        {
            try
            {
                return returningEntitiesFunction();
            }
            catch (SqlException sqlException)
            {
                var failedEntityStorageException =
                    new FailedEntityStorageException(this.entityName, sqlException);
                throw CreateAndLogCriticalDependencyException(failedEntityStorageException);
            }
            catch (Exception exception)
            {
                var failedEntityServiceException =
                    new FailedEntityServiceException(this.entityName, exception);

                throw CreateAndLogServiceException(failedEntityServiceException);
            }
        }

        protected virtual EntityValidationException CreateAndLogValidationException(Xeption exception)
        {
            var entityValidationException =
                new EntityValidationException(this.entityName, exception);

            this.loggingBroker.LogError(entityValidationException);

            return entityValidationException;
        }

        protected virtual EntityDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var entityDependencyException = new EntityDependencyException(this.entityName, exception);
            this.loggingBroker.LogCritical(entityDependencyException);

            return entityDependencyException;
        }

        protected virtual EntityDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var entityDependencyValidationException =
                new EntityDependencyValidationException(this.entityName, exception);

            this.loggingBroker.LogError(entityDependencyValidationException);

            return entityDependencyValidationException;
        }

        protected virtual EntityDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var entityDependencyException = new EntityDependencyException(this.entityName, exception);
            this.loggingBroker.LogError(entityDependencyException);

            return entityDependencyException;
        }

        protected virtual EntityServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var entityServiceException = new EntityServiceException(this.entityName, exception);
            this.loggingBroker.LogError(entityServiceException);

            return entityServiceException;
        }
    }
}
