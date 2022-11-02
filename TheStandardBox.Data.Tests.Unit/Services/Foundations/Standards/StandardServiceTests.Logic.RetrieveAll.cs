// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual void ShouldReturnEntities()
        {
            // given
            IQueryable<TEntity> randomEntities = CreateRandomEntities();
            IQueryable<TEntity> storageEntities = randomEntities;
            IQueryable<TEntity> expectedEntities = storageEntities;

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectAllEntities<TEntity>())
                    .Returns(storageEntities);

            // when
            IQueryable<TEntity> actualEntities =
                this.standardService.RetrieveAllEntities();

            // then
            actualEntities.Should().BeEquivalentTo(expectedEntities);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectAllEntities<TEntity>(),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}