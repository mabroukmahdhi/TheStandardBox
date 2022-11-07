// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using TheStandardBox.Core.Attributes.Validations;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Services.Foundations.Standards
{
    public partial class StandardService<TEntity>
    {
        protected virtual void ValidateEntityOnAdd(TEntity entity)
        {
            ValidateEntityIsNotNull(entity);

            List<(dynamic Rule, string Parameter)> validations = new()
            {
                (Rule: IsInvalid(entity.Id), Parameter: nameof(IStandardEntity.Id))
            };

            validations.AddRange(ValidateByAttribute(entity, typeof(ValidateOnAddAttribute)));

            validations.Add((Rule: IsInvalid(entity.CreatedDate), Parameter: nameof(IStandardEntity.CreatedDate)));
            validations.Add((Rule: IsInvalid(entity.UpdatedDate), Parameter: nameof(IStandardEntity.UpdatedDate)));

            Validate(validations?.ToArray());
        }

        protected virtual void ValidateEntityOnUpdate(TEntity entity)
        {
            ValidateEntityIsNotNull(entity);

            List<(dynamic Rule, string Parameter)> validations = new()
            {
                (Rule: IsInvalid(entity.Id), Parameter: nameof(IStandardEntity.Id))
            };

            validations.AddRange(ValidateByAttribute(entity, typeof(ValidateOnModifyAttribute)));

            validations.Add((Rule: IsInvalid(entity.CreatedDate), Parameter: nameof(IStandardEntity.CreatedDate)));
            validations.Add((Rule: IsInvalid(entity.UpdatedDate), Parameter: nameof(IStandardEntity.UpdatedDate)));

            Validate(validations?.ToArray());
        }

        protected virtual void ValidateEntityIsNotNull(TEntity entity)
        {
            if (entity is null)
            {
                throw new NullEntityException(this.entityName);
            }
        }

        protected virtual void ValidateEntityId(Guid entityId) =>
            Validate((Rule: IsInvalid(entityId), Parameter: nameof(IStandardEntity.Id)));

        protected virtual dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        protected virtual dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
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
            Message = "Date is required"
        };

        protected virtual List<(dynamic Rule, string Parameter)> ValidateByAttribute(TEntity entity, Type attributeType)
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

                    validations.Add((Rule: IsInvalid(val), Parameter: name));
                }
            }

            return validations;
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