// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using TheStandardBox.Core.Brokers.DateTimes;
using TheStandardBox.Core.Brokers.Entities;
using TheStandardBox.Core.Brokers.Loggings;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Data.Brokers.StandardStorages;
using TheStandardBox.Data.Services.Foundations.Standards;
using TheStandardBox.Data.Services.Standards;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace StandardApi.PoC.Tests.Unit.Services.Standards
{
    public abstract partial class StandardServiceTests<TEntity>
        where TEntity : class, IStandardEntity
    {
        protected readonly Mock<IStandardStorageBroker> standardStorageBrokerMock;
        protected readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        protected readonly Mock<ILoggingBroker> loggingBrokerMock;
        protected readonly string entityName;
        protected readonly IStandardService<TEntity> standardService;

        public StandardServiceTests()
        {
            this.standardStorageBrokerMock = new Mock<IStandardStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            var entityBroker = new StandardEntityBroker();

            this.standardService = new StandardService<TEntity>(
                standardStorageBroker: this.standardStorageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                entityBroker: entityBroker);

            entityName = entityBroker.GetEntityName<TEntity>();
        }

        protected virtual Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        protected virtual string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();


        public static TheoryData GetMinutesBeforeOrAfter()
        {
            int randomNumber =
                new IntRange(min: 2, max: 10).GetValue();

            int randomNegativeNumber =
                -1 * new IntRange(min: 2, max: 10).GetValue();

            return new TheoryData<int>
            {
                randomNumber,
                randomNegativeNumber
            };
        }

        protected virtual SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        protected virtual int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        protected virtual int GetRandomNegativeNumber() =>
            -1 * new IntRange(min: 2, max: 10).GetValue();

        protected virtual DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        protected virtual TEntity CreateRandomModifyEntity(DateTimeOffset dateTimeOffset)
        {
            int randomDaysInPast = GetRandomNegativeNumber();
            TEntity randomEntity = CreateRandomEntity(dateTimeOffset);

            randomEntity.CreatedDate =
                randomEntity.CreatedDate.AddDays(randomDaysInPast);

            return randomEntity;
        }

        protected virtual IQueryable<TEntity> CreateRandomEntities()
        {
            return CreateEntityFiller(dateTimeOffset: GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                    .AsQueryable();
        }

        protected virtual TEntity CreateRandomEntity() =>
            CreateEntityFiller(dateTimeOffset: GetRandomDateTimeOffset()).Create();

        protected virtual TEntity CreateRandomEntity(DateTimeOffset dateTimeOffset) =>
            CreateEntityFiller(dateTimeOffset).Create();

        protected virtual Filler<TEntity> CreateEntityFiller(DateTimeOffset dateTimeOffset)
        {
            var filler = new Filler<TEntity>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTimeOffset);

            // TODO: Complete the filler setup e.g. ignore related properties etc...

            return filler;
        }

        protected virtual TEntity CreateInvalidInstance(string invalidText)
        {
            return Activator.CreateInstance(typeof(TEntity)) as TEntity;
        }

        protected virtual InvalidEntityException CreateInvalidEntityExceptionOnAdd()
        {
            var invalidEntityException =
                new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                key: "Id",
                values: "Id is required");

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.CreatedDate),
                values: "Date is required");

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.UpdatedDate),
                values: "Date is required");

            return invalidEntityException;
        }

        protected virtual InvalidEntityException CreateInvalidEntityExceptionOnModify()
        {
            var invalidEntityException =
                new InvalidEntityException(entityName);

            invalidEntityException.AddData(
                 key: "Id",
                 values: "Id is required");

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.CreatedDate),
                values: "Date is required");

            invalidEntityException.AddData(
                key: nameof(IStandardEntity.UpdatedDate),
                values:
                new[] {
                    "Date is required",
                    $"Date is the same as {nameof(IStandardEntity.CreatedDate)}"
                });

            return invalidEntityException;
        }
    }
}