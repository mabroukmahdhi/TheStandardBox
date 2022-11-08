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
using FluentAssertions.Equivalency;
using TheStandardBox.Core.Brokers.Localizations;
using TheStandardBox.Core.Models.Foundations.Standards;
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
                if (property.PropertyType == typeof(DateTimeOffset))
                {
                    IViewElement elm = CreateViewElement(
                        property: property,
                        value: (DateTimeOffset)property.GetValue(entity),
                        type: ViewElementType.DatePicker);

                    viewElements.Add(elm);
                    continue;
                }

                if (property.PropertyType == typeof(string))
                {
                    IViewElement elm = CreateViewElement(
                        property: property,
                        value: (string)property.GetValue(entity),
                        type: ViewElementType.TextBox);

                    viewElements.Add(elm);
                    continue;
                }

                if (property.PropertyType == typeof(bool))
                {
                    IViewElement elm = CreateViewElement(
                        property: property,
                        value: (bool)property.GetValue(entity),
                        type: ViewElementType.ChecBox);

                    viewElements.Add(elm);
                    continue;
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

        private IViewElement CreateViewElement<T>(
            PropertyInfo property,
            T value,
            ViewElementType type)
        {
            return new ViewElement<T>
            {
                Placeholder = property.Name,
                Id = property.Name,
                Value = value,
                Type = type
            };
        }
    }
}
