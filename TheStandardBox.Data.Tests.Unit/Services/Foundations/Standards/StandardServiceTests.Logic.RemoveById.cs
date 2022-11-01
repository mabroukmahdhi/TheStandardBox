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
        public virtual async Task ShouldRemoveEntityByIdAsync()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputEntityId = randomId;
            TEntity randomEntity = CreateRandomEntity();
            TEntity storageEntity = randomEntity;
            TEntity expectedInputEntity = storageEntity;
            TEntity deletedEntity = expectedInputEntity;
            TEntity expectedEntity = deletedEntity.DeepClone();

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync(inputEntityId))
                    .ReturnsAsync(storageEntity);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.DeleteEntityAsync(expectedInputEntity))
                    .ReturnsAsync(deletedEntity);

            // when
            TEntity actualEntity = await this.smartService
                .RemoveEntityByIdAsync(inputEntityId);

            // then
            actualEntity.Should().BeEquivalentTo(expectedEntity);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync(inputEntityId),
                    Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.DeleteEntityAsync(expectedInputEntity),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}