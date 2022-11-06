// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnRemoveIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidEntityId = Guid.Empty;

            var invalidEntityException =
                new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: "Id",
                values: "Id is required");

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            // when
            ValueTask<TEntity> removeEntityByIdTask =
                this.standardService.RemoveEntityByIdAsync(invalidEntityId);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    removeEntityByIdTask.AsTask);

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.DeleteEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}