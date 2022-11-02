// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnAddIfEntityIsNullAndLogItAsync()
        {
            // given
            TEntity nullEntity = null;

            var nullEntityException =
                new NullEntityException(entityName);

            var expectedEntityValidationException =
                new EntityValidationException(entityName, nullEntityException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(nullEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(() =>
                    addEntityTask.AsTask());

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public virtual async Task ShouldThrowValidationExceptionOnAddIfEntityIsInvalidAndLogItAsync(string invalidText)
        {
            // given
            var invalidEntity = CreateInvalidInstance(invalidText);

            var invalidEntityException = CreateInvalidEntityExceptionOnAdd();

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(invalidEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(() =>
                    addEntityTask.AsTask());

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnAddIfCreateAndUpdateDatesIsNotSameAndLogItAsync()
        {
            // given
            int randomNumber = GetRandomNumber();
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            TEntity randomEntity = CreateRandomEntity(randomDateTimeOffset);
            TEntity invalidEntity = randomEntity;

            invalidEntity.UpdatedDate =
                invalidEntity.CreatedDate.AddDays(randomNumber);

            var invalidEntityException = new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.UpdatedDate),
                values: $"Date is not the same as {nameof(IStandardEntity.CreatedDate)}");

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(invalidEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(() =>
                    addEntityTask.AsTask());

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(GetMinutesBeforeOrAfter))]
        public virtual async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotRecentAndLogItAsync(
            int minutesBeforeOrAfter)
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();

            DateTimeOffset invalidDateTime =
                randomDateTimeOffset.AddMinutes(minutesBeforeOrAfter);

            TEntity randomEntity = CreateRandomEntity(invalidDateTime);
            TEntity invalidEntity = randomEntity;

            var invalidEntityException =
                new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.CreatedDate),
                values: "Date is not recent");

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            // when
            ValueTask<TEntity> addEntityTask =
                this.standardService.AddEntityAsync(invalidEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(() =>
                    addEntityTask.AsTask());

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.InsertEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
        }
    }
}