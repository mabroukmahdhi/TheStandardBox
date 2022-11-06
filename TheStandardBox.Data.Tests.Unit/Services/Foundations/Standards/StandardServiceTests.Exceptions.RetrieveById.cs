// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            var failedEntityStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedEntityStorageException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<TEntity> retrieveEntityByIdTask =
                this.standardService.RetrieveEntityByIdAsync(someId);

            EntityDependencyException actualEntityDependencyException =
                await Assert.ThrowsAsync<EntityDependencyException>(
                    retrieveEntityByIdTask.AsTask);

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
        public virtual async Task ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedEntityServiceException =
                new FailedEntityServiceException(entityName, serviceException);

            var expectedEntityServiceException =
                new EntityServiceException(entityName, failedEntityServiceException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<TEntity> retrieveEntityByIdTask =
                this.standardService.RetrieveEntityByIdAsync(someId);

            EntityServiceException actualEntityServiceException =
                await Assert.ThrowsAsync<EntityServiceException>(
                    retrieveEntityByIdTask.AsTask);

            // then
            actualEntityServiceException.Should()
                .BeEquivalentTo(expectedEntityServiceException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()),
                    Times.Once);

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