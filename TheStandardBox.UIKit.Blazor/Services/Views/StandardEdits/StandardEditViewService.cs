// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Models.Components.ViewElements;
using TheStandardBox.UIKit.Blazor.Services.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Services.Views.StandardEdits
{
    public class StandardEditViewService<TEntity> : IStandardEditViewService<TEntity>
        where TEntity : IStandardEntity
    {
        private readonly IStandardService<TEntity> standardService;

        public StandardEditViewService(IStandardService<TEntity> standardService) =>
             this.standardService = standardService;

        public List<IViewElement> GenerateViewElements(TEntity entity)
        {
            List<IViewElement> textViewElements =
            Enumerable.Empty<IViewElement>().ToList();

            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(DateTimeOffset))
                {
                    var elm = new SmartDateViewElement
                    {
                        Placeholder = property.Name,
                        Id = property.Name,
                        Value = (DateTimeOffset)property.GetValue(entity)
                    };

                    textViewElements.Add(elm);
                    continue;
                }

                if (property.PropertyType == typeof(string))
                {
                    var elm = new SmartTextViewElement
                    {
                        Placeholder = property.Name,
                        Id = property.Name,
                        Value = (string)property.GetValue(entity)
                    };

                    textViewElements.Add(elm);
                    continue;
                }
            }

            return textViewElements;
        }

        public async ValueTask UpsertEntityAsync(TEntity entity)
        {
            var date = DateTimeOffset.Now;
            entity.CreatedDate = date;
            entity.UpdatedDate = date;

            await standardService.AddEntityAsync(entity);
        }
    }
}
