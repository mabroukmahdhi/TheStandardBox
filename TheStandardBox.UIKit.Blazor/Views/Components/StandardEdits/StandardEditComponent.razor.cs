// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Models.Components.Containers;
using TheStandardBox.UIKit.Blazor.Models.Components.ViewElements;
using TheStandardBox.UIKit.Blazor.Services.Views.StandardEdits;
using TheStandardBox.UIKit.Blazor.Views.Components.Localizations;

namespace TheStandardBox.UIKit.Blazor.Views.Components.StandardEdits
{
    public partial class StandardEditComponent<TEntity> : LocalizedComponent
        where TEntity : IStandardEntity
    {
        [Inject]
        public IStandardEditViewService<TEntity> EditViewService { get; set; }

        [Parameter]
        public int Columns { get; set; } = 1;

        protected int ColumnCount => Columns > 0 ? Columns : 1;

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

            textViewElements = EditViewService.GenerateViewElements(Entity);

            State = ComponentState.Content;
        }

        private async void OnAddClicked()
        {
            try
            {
                AddErrorMessage = string.Empty;

                await EditViewService.UpsertEntityAsync(Entity, textViewElements);
            }
            catch (Exception ex)
            {
                AddErrorMessage = ex.Message;
            }
            StateHasChanged();
        }
    }
}
