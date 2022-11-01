﻿// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TheStandardBox.Core.Attributes.Validations;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.Core.Models.Foundations.Standards.Exceptions;

namespace TheStandardBox.Data.Services.Standards
{
    public partial class StandardService<TEntity>
    {
        private IEnumerable<(dynamic Rule, string Parameter)> SharedValidations(TEntity entity)
        {
            List<(dynamic Rule, string Parameter)> sharedValidations =
                new List<(dynamic Rule, string Parameter)>{
                    (Rule: IsInvalid(entity.Id), Parameter: nameof(IStandardEntity.Id)),
                    (Rule: IsInvalid(entity.CreatedDate), Parameter: nameof(IStandardEntity.CreatedDate)),
                    (Rule: IsInvalid(entity.UpdatedDate), Parameter: nameof(IStandardEntity.UpdatedDate))
                };

            return sharedValidations;
        }

        private IEnumerable<(dynamic Rule, string Parameter)> ValidateByAttribute(TEntity entity, Type attributeType)
        {
            List<(dynamic Rule, string Parameter)> validations = new();

            var properties = entity.GetType().GetProperties().Where(p =>
                Attribute.IsDefined(p, attributeType));

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    var val = property.GetValue(entity) as string;
                    var name = property.Name;

                    validations.Add((Rule: IsInvalid(val), Parameter: name));
                }
            }

            return validations;
        }

        private void ValidateEntityOnAdd(TEntity entity)
        {
            ValidateEntityIsNotNull(entity);

            List<(dynamic Rule, string Parameter)> validations = new();

            validations.AddRange(SharedValidations(entity));
            validations.AddRange(ValidateByAttribute(entity, typeof(ValidateOnAddAttribute)));
            validations.Add((Rule: IsNotSame(
                    firstDate: entity.UpdatedDate,
                    secondDate: entity.CreatedDate,
                    secondDateName: nameof(IStandardEntity.CreatedDate)),
                Parameter: nameof(IStandardEntity.UpdatedDate)));

            validations.Add((Rule: IsNotRecent(entity.CreatedDate), Parameter: nameof(IStandardEntity.CreatedDate)));

            Validate(validations?.ToArray());
        }

        private void ValidateEntityOnModify(TEntity entity)
        {
            ValidateEntityIsNotNull(entity);

            List<(dynamic Rule, string Parameter)> validations = new();

            validations.AddRange(SharedValidations(entity));
            validations.AddRange(ValidateByAttribute(entity, typeof(ValidateOnModifyAttribute)));
            validations.Add((Rule: IsSame(
                    firstDate: entity.UpdatedDate,
                    secondDate: entity.CreatedDate,
                    secondDateName: nameof(IStandardEntity.CreatedDate)),
                Parameter: nameof(IStandardEntity.UpdatedDate)));

            validations.Add((Rule: IsNotRecent(entity.UpdatedDate), Parameter: nameof(entity.UpdatedDate)));

            Validate(validations?.ToArray());
        }

        public void ValidateEntityId(Guid entityId) =>
            Validate((Rule: IsInvalid(entityId), Parameter: nameof(IStandardEntity.Id)));

        private void ValidateStorageEntity(TEntity maybeEntity, Guid entityId)
        {
            if (maybeEntity is null)
            {
                throw new NotFoundEntityException(this.entityName, entityId);
            }
        }

        private void ValidateEntityIsNotNull(TEntity entity)
        {
            if (entity is null)
            {
                throw new NullEntityException(this.entityName);
            }
        }

        private void ValidateAgainstStorageEntityOnModify(TEntity inputEntity, TEntity storageEntity)
        {
            Validate(
                (Rule: IsNotSame(
                    firstDate: inputEntity.CreatedDate,
                    secondDate: storageEntity.CreatedDate,
                    secondDateName: nameof(IStandardEntity.CreatedDate)),
                Parameter: nameof(IStandardEntity.CreatedDate)),

                (Rule: IsSame(
                    firstDate: inputEntity.UpdatedDate,
                    secondDate: storageEntity.UpdatedDate,
                    secondDateName: nameof(IStandardEntity.UpdatedDate)),
                Parameter: nameof(IStandardEntity.UpdatedDate)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(object obj) => new
        {
            Condition = obj == default,
            Message = "Field is required"
        };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTimeOffset();
            TimeSpan timeDifference = currentDateTime.Subtract(date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }

        private static void Validate(
            IEnumerable<(dynamic Rule, string Parameter)> sharedValidations,
            params (dynamic Rule, string Parameter)[] validations)
        {
            List<(dynamic Rule, string Parameter)> allValidations = new List<(dynamic Rule, string Parameter)>();
            allValidations.AddRange(sharedValidations);
            allValidations.AddRange(validations);

            Validate(allValidations.ToArray());
        }

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidEntityException = new InvalidEntityException(this.entityName);

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidEntityException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidEntityException.ThrowIfContainsErrors();
        }
    }
}