// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using TheStandardBox.Core.Extensions;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            SqlException sqlException = GetSqlException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(sqlException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(randomEntity);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(randomEntity.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedEntityDependencyException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(randomEntity),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async void ShouldThrowValidationExceptionOnModifyIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            TEntity someEntity = CreateRandomEntity();
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidEntityReferenceException =
                new InvalidEntityReferenceException(entityName, foreignKeyConstraintConflictException);

            EntityDependencyValidationException expectedEntityDependencyValidationException =
                new EntityDependencyValidationException(entityName, invalidEntityReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(foreignKeyConstraintConflictException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(someEntity);

            EntityDependencyValidationException actualEntityDependencyValidationException =
                await Assert.ThrowsAsync<EntityDependencyValidationException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityDependencyValidationException.Should()
                .BeEquivalentTo(expectedEntityDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(someEntity.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedEntityDependencyValidationException))),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(someEntity),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            var databaseUpdateException = new DbUpdateException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, databaseUpdateException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(randomEntity);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(randomEntity.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityDependencyException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(randomEntity),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowDependencyValidationExceptionOnModifyIfDbUpdateConcurrencyErrorOccursAndLogAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            var databaseUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedEntityException =
                new LockedEntityException(entityName, databaseUpdateConcurrencyException);

            var expectedEntityDependencyValidationException =
                new EntityDependencyValidationException(entityName, lockedEntityException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databaseUpdateConcurrencyException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(randomEntity);

            EntityDependencyValidationException actualEntityDependencyValidationException =
                await Assert.ThrowsAsync<EntityDependencyValidationException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityDependencyValidationException.Should()
                .BeEquivalentTo(expectedEntityDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(randomEntity.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityDependencyValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(randomEntity),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            var serviceException = new Exception();

            var failedEntityServiceException =
                new FailedEntityServiceException(entityName, serviceException);

            var expectedEntityServiceException =
                new EntityServiceException(entityName, failedEntityServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(serviceException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(randomEntity);

            EntityServiceException actualEntityServiceException =
                await Assert.ThrowsAsync<EntityServiceException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityServiceException.Should()
                .BeEquivalentTo(expectedEntityServiceException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(randomEntity.Id),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityServiceException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(randomEntity),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}