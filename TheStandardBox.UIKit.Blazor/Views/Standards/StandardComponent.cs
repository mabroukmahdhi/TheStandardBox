// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Services.Views.Renderings;

namespace TheStandardBox.UIKit.Blazor.Views.Standards
{
    public class StandardComponent : ComponentBase
    {
        [Inject]
        public IStandardRenderingService RenderingService { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public bool IsVisible { get; set; } = true;

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public Dictionary<string, object> Attributes { get; set; }

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }

        public void SetAttributes(Dictionary<string, object> attributes) =>
            this.Attributes = attributes;

        protected virtual Dictionary<string, object> GetAttributes()
            => this.Attributes;

        protected virtual string GetCssClass()
            => CssClass;

        protected virtual string GetStyle()
            => Style;

        protected virtual RenderFragment CreateComponent() =>
            builder => { };
    }
}