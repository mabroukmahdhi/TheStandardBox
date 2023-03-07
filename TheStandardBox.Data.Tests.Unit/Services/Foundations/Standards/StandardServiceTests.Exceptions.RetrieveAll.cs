// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using Xunit;

namespace TheStandardBox.Data.Tests.Unit.Services.Foundations.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlException();

            var failedStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedStorageException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectAllEntities<TEntity>())
                    .Throws(sqlException);

            // when
            Action retrieveAllEntitiesAction = () =>
                this.standardService.RetrieveAllEntities();

            EntityDependencyException actualEntityDependencyException =
                Assert.Throws<EntityDependencyException>(retrieveAllEntitiesAction);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectAllEntities<TEntity>(),
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
        public virtual void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string exceptionMessage = GetRandomMessage();
            var serviceException = new Exception(exceptionMessage);

            var failedEntityServiceException =
                new FailedEntityServiceException(entityName, serviceException);

            var expectedEntityServiceException =
                new EntityServiceException(entityName, failedEntityServiceException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectAllEntities<TEntity>())
                    .Throws(serviceException);

            // when
            Action retrieveAllEntitiesAction = () =>
                this.standardService.RetrieveAllEntities();

            EntityServiceException actualEntityServiceException =
                Assert.Throws<EntityServiceException>(retrieveAllEntitiesAction);

            // then
            actualEntityServiceException.Should()
                .BeEquivalentTo(expectedEntityServiceException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectAllEntities<TEntity>(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityServiceException))),
                        Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}