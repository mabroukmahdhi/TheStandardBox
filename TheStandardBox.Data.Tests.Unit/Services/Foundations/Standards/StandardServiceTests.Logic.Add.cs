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
        public virtual async Task ShouldAddEntityAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset =
                GetRandomDateTimeOffset();

            TEntity randomEntity = CreateRandomEntity(randomDateTimeOffset);
            TEntity inputEntity = randomEntity;
            TEntity storageEntity = inputEntity;
            TEntity expectedEntity = storageEntity.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.InsertEntityAsync(inputEntity))
                    .ReturnsAsync(storageEntity);

            // when
            TEntity actualEntity = await this.smartService
                .AddEntityAsync(inputEntity);

            // then
            actualEntity.Should().BeEquivalentTo(expectedEntity);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(inputEntity),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}