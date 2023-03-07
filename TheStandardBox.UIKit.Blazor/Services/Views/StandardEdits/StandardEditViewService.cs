// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheStandardBox.Core.Brokers.Localizations;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Attributes;
using TheStandardBox.UIKit.Blazor.Models.Components.ViewElements;
using TheStandardBox.UIKit.Blazor.Services.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Services.Views.StandardEdits
{
    public class StandardEditViewService<TEntity> : IStandardEditViewService<TEntity>
        where TEntity : IStandardEntity
    {
        private readonly IStandardService<TEntity> standardService;
        private readonly ILocalizationBroker localizationBroker;

        public StandardEditViewService(
            IStandardService<TEntity> standardService,
            ILocalizationBroker localizationBroker)
        {
            this.standardService = standardService;
            this.localizationBroker = localizationBroker;
        }

        public List<IViewElement> GenerateViewElements(TEntity entity)
        {
            List<IViewElement> viewElements =
            Enumerable.Empty<IViewElement>().ToList();

            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                var fieldAttribute = property.GetCustomAttribute<FieldAttribute>();

                if (fieldAttribute != null)
                {
                    var id = string.IsNullOrWhiteSpace(fieldAttribute.Name)
                        ? property.Name
                        : fieldAttribute.Name;

                    id = $"StandardEdit_{id}";

                    if (property.PropertyType == typeof(DateTimeOffset))
                    {
                        IViewElement elm = CreateDateViewElement(
                            id: id,
                            value: (DateTimeOffset)property.GetValue(entity),
                            defaultPlaceholder: property.Name);

                        viewElements.Add(elm);
                        continue;
                    }

                    if (property.PropertyType == typeof(string))
                    {
                        IViewElement elm = CreateTextViewElement(
                            id: id,
                            value: (string)property.GetValue(entity),
                            defaultPlaceholder: property.Name);

                        viewElements.Add(elm);
                        continue;
                    }

                    if (property.PropertyType == typeof(bool))
                    {
                        IViewElement elm = CreateCheckboxViewElement(
                            id: id,
                            value: (bool)property.GetValue(entity),
                            defaultPlaceholder: property.Name);

                        viewElements.Add(elm);
                        continue;
                    }
                }
            }

            return viewElements;
        }

        public async ValueTask UpsertEntityAsync(TEntity entity, List<IViewElement> viewElements)
        {
            foreach (var item in viewElements)
            {
                entity.GetType()
                    .GetProperty(item.Id)
                    .SetValue(entity, item.GetValue());
            }

            var date = DateTimeOffset.Now;
            entity.CreatedDate = date;
            entity.UpdatedDate = date;

            await standardService.AddEntityAsync(entity);
        }

        private IViewElement CreateTextViewElement(
            string id,
            string value,
            string defaultPlaceholder)
        {
            return new TextViewElement
            {
                Placeholder = GetPlaceholder(id, defaultPlaceholder),
                Id = id,
                Value = value
            };
        }

        private IViewElement CreateDateViewElement(
            string id,
            DateTimeOffset value,
            string defaultPlaceholder)
        {
            return new DateViewElement
            {
                Placeholder = GetPlaceholder(id, defaultPlaceholder),
                Id = id,
                Value = value
            };
        }

        private IViewElement CreateCheckboxViewElement(
            string id,
            bool value,
            string defaultPlaceholder)
        {
            return new CheckboxViewElement
            {
                Placeholder = GetPlaceholder(id, defaultPlaceholder),
                Id = id,
                Value = value
            };
        }

        private string GetPlaceholder(string id, string defaultValue) =>
            string.IsNullOrEmpty(this.localizationBroker[id])
                ? defaultValue
                : this.localizationBroker[id];
    }
}