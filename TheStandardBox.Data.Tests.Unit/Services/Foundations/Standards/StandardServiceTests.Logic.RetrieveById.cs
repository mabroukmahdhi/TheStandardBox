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
using TheStandardBox.Core.Extensions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldRetrieveEntityByIdAsync()
        {
            // given
            TEntity randomEntity = CreateRandomEntity();
            TEntity inputEntity = randomEntity;
            TEntity storageEntity = randomEntity;
            TEntity expectedEntity = storageEntity.DeepClone();

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(inputEntity.GetPrimaryKeys()))
                    .ReturnsAsync(storageEntity);

            // when
            TEntity actualEntity =
                await this.standardService.RetrieveEntityByIdAsync(inputEntity.GetPrimaryKey<TEntity, Guid>());

            // then
            actualEntity.Should().BeEquivalentTo(expectedEntity);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(inputEntity.GetPrimaryKeys()),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}