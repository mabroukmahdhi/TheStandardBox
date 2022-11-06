// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            var invalidEntityId = Guid.Empty;

            var invalidEntityException =
                new InvalidEntityException(this.entityName);

            invalidEntityException.AddData(
                key: "Id",
                values: "Id is required");

            var expectedEntityValidationException =
                new EntityValidationException(this.entityName, invalidEntityException);

            // when
            ValueTask<TEntity> retrieveEntityByIdTask =
                this.standardService.RetrieveEntityByIdAsync(invalidEntityId);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    retrieveEntityByIdTask.AsTask);

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfEntityIsNotFoundAndLogItAsync()
        {
            //given
            Guid someEntityId = Guid.NewGuid();
            TEntity noEntity = null;

            var notFoundEntityException =
                new NotFoundEntityException(this.entityName, someEntityId);

            var expectedEntityValidationException =
                new EntityValidationException(this.entityName, notFoundEntityException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()))
                    .ReturnsAsync(noEntity);

            //when
            ValueTask<TEntity> retrieveEntityByIdTask =
                this.standardService.RetrieveEntityByIdAsync(someEntityId);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    retrieveEntityByIdTask.AsTask);

            //then
            actualEntityValidationException.Should().BeEquivalentTo(expectedEntityValidationException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(It.IsAny<Guid>()),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}