// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public class BasicComponent : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        public bool IsEnabled => IsDisabled is false;

        [Parameter]
        public string HoverStyle { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public bool IsVisible { get; set; } = true;

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

        public void SetColor(string color) =>
            this.Color = color;

        public void SetAttributes(Dictionary<string, object> attributes) =>
            this.Attributes = attributes;

        protected virtual Dictionary<string, object> GetAttributes() =>
            this.Attributes;

        protected virtual string GetCssClass() => CssClass;

        protected virtual string GetStyle() => Style;
    }
}