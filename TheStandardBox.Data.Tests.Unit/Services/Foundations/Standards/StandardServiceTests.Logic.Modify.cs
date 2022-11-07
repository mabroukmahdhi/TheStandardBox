// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldModifyEntityAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            TEntity randomEntity = CreateRandomModifyEntity(randomDateTimeOffset);
            TEntity inputEntity = randomEntity;
            TEntity storageEntity = inputEntity.DeepClone();
            storageEntity.UpdatedDate = randomEntity.CreatedDate;
            TEntity updatedEntity = inputEntity;
            TEntity expectedEntity = updatedEntity.DeepClone();
            var entityId = inputEntity.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(entityId))
                    .ReturnsAsync(storageEntity);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.UpdateEntityAsync(inputEntity))
                    .ReturnsAsync(updatedEntity);

            // when
            TEntity actualEntity =
                await this.standardService.ModifyEntityAsync(inputEntity);

            // then
            actualEntity.Should().BeEquivalentTo(expectedEntity);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(entityId),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(inputEntity),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}