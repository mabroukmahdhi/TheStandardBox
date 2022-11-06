// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowCriticalDependencyExceptionOnRemoveIfSqlErrorOccursAndLogItAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            SqlException sqlException = GetSqlException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(randomEntity.Id))
                    .Throws(sqlException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.RemoveEntityByIdAsync(randomEntity.Id);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    addEntityTask.AsTask);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(randomEntity.Id),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedEntityDependencyException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.DeleteEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Never);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowDependencyValidationOnRemoveIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
        {
            // given
            Guid someEntityId = Guid.NewGuid();

            var databaseUpdateConcurrencyException =
                new DbUpdateConcurrencyException();

            var lockedEntityException =
                new LockedEntityException(entityName, databaseUpdateConcurrencyException);

            var expectedEntityDependencyValidationException =
                new EntityDependencyValidationException(entityName, lockedEntityException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()))
                    .ThrowsAsync(databaseUpdateConcurrencyException);

            // when
            ValueTask<TEntity> removeEntityByIdTask =
                this.standardService.RemoveEntityByIdAsync(someEntityId);

            EntityDependencyValidationException actualEntityDependencyValidationException =
                await Assert.ThrowsAsync<EntityDependencyValidationException>(
                    removeEntityByIdTask.AsTask);

            // then
            actualEntityDependencyValidationException.Should()
                .BeEquivalentTo(expectedEntityDependencyValidationException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityDependencyValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.DeleteEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowDependencyExceptionOnRemoveWhenSqlExceptionOccursAndLogItAsync()
        {
            // given
            Guid someEntityId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<TEntity> deleteEntityTask =
                this.standardService.RemoveEntityByIdAsync(someEntityId);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    deleteEntityTask.AsTask);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedEntityDependencyException))),
                        Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowServiceExceptionOnRemoveIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someEntityId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedEntityServiceException =
                new FailedEntityServiceException(entityName, serviceException);

            var expectedEntityServiceException =
                new EntityServiceException(entityName, failedEntityServiceException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<TEntity> removeEntityByIdTask =
                this.standardService.RemoveEntityByIdAsync(someEntityId);

            EntityServiceException actualEntityServiceException =
                await Assert.ThrowsAsync<EntityServiceException>(
                    removeEntityByIdTask.AsTask);

            // then
            actualEntityServiceException.Should()
                .BeEquivalentTo(expectedEntityServiceException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()),
                        Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityServiceException))),
                        Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}