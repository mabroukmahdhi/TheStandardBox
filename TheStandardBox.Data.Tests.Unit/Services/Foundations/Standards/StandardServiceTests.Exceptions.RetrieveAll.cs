// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlException();

            var failedStorageException =
                new FailedEntityStorageException(entityName, sqlException);

            var expectedEntityDependencyException =
                new EntityDependencyException(entityName, failedStorageException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectAllEntities())
                    .Throws(sqlException);

            // when
            Action retrieveAllEntitysAction = () =>
                this.smartService.RetrieveAllEntities();

            EntityDependencyException actualEntityDependencyException =
                Assert.Throws<EntityDependencyException>(retrieveAllEntitysAction);

            // then
            actualEntityDependencyException.Should()
                .BeEquivalentTo(expectedEntityDependencyException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectAllEntities(),
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
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string exceptionMessage = GetRandomMessage();
            var serviceException = new Exception(exceptionMessage);

            var failedEntityServiceException =
                new FailedEntityServiceException(entityName, serviceException);

            var expectedEntityServiceException =
                new EntityServiceException(entityName, failedEntityServiceException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectAllEntities())
                    .Throws(serviceException);

            // when
            Action retrieveAllEntitysAction = () =>
                this.smartService.RetrieveAllEntities();

            EntityServiceException actualEntityServiceException =
                Assert.Throws<EntityServiceException>(retrieveAllEntitysAction);

            // then
            actualEntityServiceException.Should()
                .BeEquivalentTo(expectedEntityServiceException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectAllEntities(),
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