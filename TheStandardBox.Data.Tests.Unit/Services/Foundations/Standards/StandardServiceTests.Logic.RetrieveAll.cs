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
        public void ShouldReturnEntitys()
        {
            // given
            IQueryable<TEntity> randomEntitys = CreateRandomEntitys();
            IQueryable<TEntity> storageEntitys = randomEntitys;
            IQueryable<TEntity> expectedEntitys = storageEntitys;

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectAllEntities())
                    .Returns(storageEntitys);

            // when
            IQueryable<TEntity> actualEntitys =
                this.smartService.RetrieveAllEntities();

            // then
            actualEntitys.Should().BeEquivalentTo(expectedEntitys);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectAllEntities(),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}