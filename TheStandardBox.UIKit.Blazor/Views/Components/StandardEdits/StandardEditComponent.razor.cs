// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Models.Components.ViewElements;
using TheStandardBox.UIKit.Blazor.Services.Foundations.Standards;

namespace TheStandardBox.UIKit.Blazor.Views.Components.StandardEdits
{
    public partial class StandardEditComponent<TEntity> : ComponentBase
        where TEntity : IStandardEntity
    {
        [Inject]
        public IStandardService<TEntity> SmartService { get; set; }

        [Parameter]
        public TEntity Entity { get; set; }

        [Parameter]
        public EventCallback<TEntity> EntityChanged { get; set; }

        private List<IViewElement> textViewElements;

        private string AddErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            if (Entity == null)
            {
                Entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
                Entity.Id = Guid.NewGuid();
            }

            textViewElements = new List<SmartTextViewElement>()
                .Cast<IViewElement>().ToList();

            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(DateTimeOffset))
                {
                    var elm = new SmartDateViewElement
                    {
                        Placeholder = property.Name,
                        Id = property.Name,
                        Value = (DateTimeOffset)property.GetValue(Entity)
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
                        Value = (string)property.GetValue(Entity)
                    };

                    textViewElements.Add(elm);
                    continue;
                }
            }
        }

        private async void OnAddClicked()
        {
            try
            {
                foreach (var item in textViewElements)
                {
                    Entity.GetType().GetProperty(item.Id).SetValue(Entity, item.GetValue());
                }
                AddErrorMessage = string.Empty;
                var date = DateTimeOffset.Now;
                Entity.CreatedDate = date;
                Entity.UpdatedDate = date;

                await SmartService.AddEntityAsync(Entity);
            }
            catch (Exception ex)
            {
                AddErrorMessage = ex.Message;
            }
            StateHasChanged();
        }
    }
}
