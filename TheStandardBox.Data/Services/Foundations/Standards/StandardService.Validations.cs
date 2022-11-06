// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TheStandardBox.Core.Attributes.Annotations;
using TheStandardBox.Core.Attributes.Validations;
using TheStandardBox.Core.Models.Foundations.Bases;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.Data.Services.Standards
{
    public partial class StandardService<TEntity>
    {
        protected virtual IEnumerable<(dynamic Rule, string Parameter)> SharedValidations(TEntity entity)
        {
            List<(dynamic Rule, string Parameter)> sharedValidations =
                new()
                {
                    (Rule: IsInvalid(entity.CreatedDate), Parameter: nameof(IBaseEntity.CreatedDate)),
                    (Rule: IsInvalid(entity.UpdatedDate), Parameter: nameof(IBaseEntity.UpdatedDate))
                };

            return sharedValidations;
        }

        protected virtual IEnumerable<(dynamic Rule, string Parameter)> ValidateByAttribute(TEntity entity, Type attributeType)
        {
            List<(dynamic Rule, string Parameter)> validations = new();

            var properties = entity.GetType().GetProperties().Where(p =>
                Attribute.IsDefined(p, attributeType));

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    var val = property.GetValue(entity);
                    var name = property.Name;

                    if (property.PropertyType == typeof(string))
                    {
                        validations.Add((Rule: IsInvalid(val as string), Parameter: name));
                    }
                    else if (property.PropertyType == typeof(DateTimeOffset))
                    {
                        validations.Add((Rule: IsInvalid((DateTimeOffset)val), Parameter: name));
                    }
                    else if (property.PropertyType == typeof(Guid))
                    {
                        validations.Add((Rule: IsInvalid((Guid)val), Parameter: name));
                    }
                    else
                    {
                        validations.Add((Rule: IsInvalid(val), Parameter: name));
                    }
                }
            }

            return validations;
        }

        protected virtual void ValidateEntityOnAdd(TEntity entity)
        {
            ValidateEntityIsNotNull(entity);

            List<(dynamic Rule, string Parameter)> validations = new();

            validations.AddRange(SharedValidations(entity));
            validations.AddRange(ValidateByAttribute(entity, typeof(PrimaryKeyAttribute)));
            validations.AddRange(ValidateByAttribute(entity, typeof(ValidateOnAddAttribute)));
            validations.Add((Rule: IsNotSame(
                    firstDate: entity.UpdatedDate,
                    secondDate: entity.CreatedDate,
                    secondDateName: nameof(IStandardEntity.CreatedDate)),
                Parameter: nameof(IStandardEntity.UpdatedDate)));

            validations.Add((Rule: IsNotRecent(entity.CreatedDate), Parameter: nameof(IStandardEntity.CreatedDate)));

            Validate(validations?.ToArray());
        }

        protected virtual void ValidateEntityOnModify(TEntity entity)
        {
            ValidateEntityIsNotNull(entity);

            List<(dynamic Rule, string Parameter)> validations = new();

            validations.AddRange(SharedValidations(entity));
            validations.AddRange(ValidateByAttribute(entity, typeof(ValidateOnModifyAttribute)));
            validations.Add((Rule: IsSame(
                    firstDate: entity.UpdatedDate,
                    secondDate: entity.CreatedDate,
                    secondDateName: nameof(IBaseEntity.CreatedDate)),
                Parameter: nameof(IBaseEntity.UpdatedDate)));

            validations.Add((Rule: IsNotRecent(entity.UpdatedDate), Parameter: nameof(entity.UpdatedDate)));

            Validate(validations?.ToArray());
        }

        protected virtual void ValidateEntityId(Guid entityId) =>
            Validate((Rule: IsInvalid(entityId), Parameter: "Id"));

        protected virtual void ValidateStorageEntity(TEntity maybeEntity, params object[] entityIds)
        {
            if (maybeEntity is null)
            {
                throw new NotFoundEntityException(this.entityName, entityIds);
            }
        }

        protected virtual void ValidateEntityIsNotNull(TEntity entity)
        {
            if (entity is null)
            {
                throw new NullEntityException(this.entityName);
            }
        }

        protected virtual void ValidateAgainstStorageEntityOnModify(TEntity inputEntity, TEntity storageEntity)
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

        protected virtual dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        protected virtual dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        protected virtual dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        protected virtual dynamic IsInvalid(object obj) => new
        {
            Condition = obj == default,
            Message = "Field is required"
        };

        protected virtual dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        protected virtual dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        protected virtual dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        protected virtual dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        protected virtual bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime = this.dateTimeBroker.GetCurrentDateTimeOffset();
            TimeSpan timeDifference = currentDateTime.Subtract(date);

            return timeDifference.TotalSeconds is > 60 or < 0;
        }

        protected virtual void Validate(
            IEnumerable<(dynamic Rule, string Parameter)> sharedValidations,
            params (dynamic Rule, string Parameter)[] validations)
        {
            List<(dynamic Rule, string Parameter)> allValidations = new List<(dynamic Rule, string Parameter)>();
            allValidations.AddRange(sharedValidations);
            allValidations.AddRange(validations);

            Validate(allValidations.ToArray());
        }

        protected virtual void Validate(params (dynamic Rule, string Parameter)[] validations)
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