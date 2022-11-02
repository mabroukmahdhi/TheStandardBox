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
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
    {
        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnModifyIfEntityIsNullAndLogItAsync()
        {
            // given
            TEntity nullEntity = null;
            var nullEntityException = new NullEntityException(entityName);

            var expectedEntityValidationException =
                new EntityValidationException(entityName, nullEntityException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(nullEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Never);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public virtual async Task ShouldThrowValidationExceptionOnModifyIfEntityIsInvalidAndLogItAsync(string invalidText)
        {
            // given 
            var invalidEntity = CreateInvalidInstance(invalidText);

            var invalidEntityException = CreateInvalidEntityExceptionOnModify();

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(invalidEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    modifyEntityTask.AsTask);

            //then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once());

            this.standardStorageBrokerMock.Verify(broker =>
                broker.UpdateEntityAsync(It.IsAny<TEntity>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnModifyIfUpdatedDateIsSameAsCreatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            TEntity randomEntity = CreateRandomEntity(randomDateTimeOffset);
            TEntity invalidEntity = randomEntity;
            var invalidEntityException = new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.UpdatedDate),
                values: $"Date is the same as {nameof(IStandardEntity.CreatedDate)}");

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(invalidEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(invalidEntity.Id),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnModifyIfEntityDoesNotExistAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            TEntity randomEntity = CreateRandomModifyEntity(randomDateTimeOffset);
            TEntity nonExistEntity = randomEntity;
            TEntity nullEntity = null;

            var notFoundEntityException =
                new NotFoundEntityException(entityName, nonExistEntity.Id);

            var expectedEntityValidationException =
                new EntityValidationException(entityName, notFoundEntityException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(nonExistEntity.Id))
                .ReturnsAsync(nullEntity);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                .Returns(randomDateTimeOffset);

            // when 
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(nonExistEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(nonExistEntity.Id),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnModifyIfStorageCreatedDateNotSameAsCreatedDateAndLogItAsync()
        {
            // given
            int randomNumber = GetRandomNegativeNumber();
            int randomMinutes = randomNumber;
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            TEntity randomEntity = CreateRandomModifyEntity(randomDateTimeOffset);
            TEntity invalidEntity = randomEntity.DeepClone();
            TEntity storageEntity = invalidEntity.DeepClone();
            storageEntity.CreatedDate = storageEntity.CreatedDate.AddMinutes(randomMinutes);
            storageEntity.UpdatedDate = storageEntity.UpdatedDate.AddMinutes(randomMinutes);
            var invalidEntityException = new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.CreatedDate),
                values: $"Date is not the same as {nameof(IStandardEntity.CreatedDate)}");

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(invalidEntity.Id))
                .ReturnsAsync(storageEntity);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                .Returns(randomDateTimeOffset);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(invalidEntity);

            EntityValidationException actualEntityValidationException =
                await Assert.ThrowsAsync<EntityValidationException>(
                    modifyEntityTask.AsTask);

            // then
            actualEntityValidationException.Should()
                .BeEquivalentTo(expectedEntityValidationException);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(invalidEntity.Id),
                    Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedEntityValidationException))),
                       Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public virtual async Task ShouldThrowValidationExceptionOnModifyIfStorageUpdatedDateSameAsUpdatedDateAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTimeOffset = GetRandomDateTimeOffset();
            TEntity randomEntity = CreateRandomModifyEntity(randomDateTimeOffset);
            TEntity invalidEntity = randomEntity;
            TEntity storageEntity = randomEntity.DeepClone();

            var invalidEntityException = new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.UpdatedDate),
                values: $"Date is the same as {nameof(IStandardEntity.UpdatedDate)}");

            var expectedEntityValidationException =
                new EntityValidationException(entityName, invalidEntityException);

            this.standardStorageBrokerMock.Setup(broker =>
                broker.SelectEntityByIdAsync<TEntity>(invalidEntity.Id))
                .ReturnsAsync(storageEntity);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDateTimeOffset);

            // when
            ValueTask<TEntity> modifyEntityTask =
                this.standardService.ModifyEntityAsync(invalidEntity);

            // then
            await Assert.ThrowsAsync<EntityValidationException>(
                modifyEntityTask.AsTask);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedEntityValidationException))),
                        Times.Once);

            this.standardStorageBrokerMock.Verify(broker =>
                broker.SelectEntityByIdAsync<TEntity>(invalidEntity.Id),
                    Times.Once);

            this.standardStorageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}