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
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            TEntity someEntity = CreateRandomEntity();
            SqlException sqlException = GetSqlException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(sqlException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(someEntity);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    addEntityTask.AsTask);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedEntityDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowDependencyValidationExceptionOnAddIfEntityAlreadyExsitsAndLogItAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            TEntity alreadyExistsEntity = randomEntity;
            string randomMessage = GetRandomMessage();

            var duplicateKeyException =
                new DuplicateKeyException(randomMessage);

            var alreadyExistsEntityException =
                new AlreadyExistsEntityException(entityName, duplicateKeyException);

            var expectedEntityDependencyValidationException =
                new EntityDependencyValidationException(entityName, alreadyExistsEntityException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(alreadyExistsEntity);

            // then
            EntityDependencyValidationException actualEntityDependencyValidationException =
                await Assert.ThrowsAsync<EntityDependencyValidationException>(
                    addEntityTask.AsTask);

            actualEntityDependencyValidationException.Should()
                .BeEquivalentTo(expectedEntityDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityDependencyValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async void ShouldThrowValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            TEntity someEntity = CreateRandomEntity();
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidEntityReferenceException =
                new InvalidEntityReferenceException(entityName, foreignKeyConstraintConflictException);

            var expectedEntityValidationException =
                new EntityDependencyValidationException(entityName, invalidEntityReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(foreignKeyConstraintConflictException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(someEntity);

            // then
            EntityDependencyValidationException actualEntityDependencyValidationException =
                await Assert.ThrowsAsync<EntityDependencyValidationException>(
                    addEntityTask.AsTask);

            actualEntityDependencyValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(someEntity),
                    Times.Never());

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            TEntity someEntity = CreateRandomEntity();

            var databaseUpdateException =
                new DbUpdateException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, databaseUpdateException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(someEntity);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    addEntityTask.AsTask);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            TEntity someEntity = CreateRandomEntity();
            var serviceException = new Exception();

            var failedEntityServiceException =
                new FailedEntityServiceException(entityName, serviceException);

            var expectedEntityServiceException =
                new EntityServiceException(entityName, failedEntityServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(serviceException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(someEntity);

            EntityServiceException actualEntityServiceException =
                await Assert.ThrowsAsync<EntityServiceException>(
                    addEntityTask.AsTask);

            // then
            actualEntityServiceException.Should()
                .BeEquivalentTo(expectedEntityServiceException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityServiceException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}