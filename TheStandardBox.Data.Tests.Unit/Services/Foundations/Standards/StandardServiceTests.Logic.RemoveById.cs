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

namespace TheStandardBox.Data.Tests.Unit.Services.Foundations.Standards
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
                broker.SelectEntityByIdAsync<TEntity>(inputEntityId))
                    .ReturnsAsync(storageEntity);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.DeleteEntityAsync(expectedInputEntity))
                    .ReturnsAsync(deletedEntity);

            // when
            TEntity actualEntity = await this.standardService
                .RemoveEntityByIdAsync(inputEntityId);

            // then
            actualEntity.Should().BeEquivalentTo(expectedEntity);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(inputEntityId),
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