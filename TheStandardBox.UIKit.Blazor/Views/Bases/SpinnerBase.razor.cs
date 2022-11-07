// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class SpinnerBase : ComponentBase
    {
        [Parameter]
        public bool IsVisible { get; set; }

        [Parameter]
        public string Value { get; set; }

        public void Show()
        {
            IsVisible = true;
            InvokeAsync(StateHasChanged);
        }

        public void Hide()
        {
            IsVisible = false;
            InvokeAsync(StateHasChanged);
        }

        public void SetValue(string value)
        {
            Value = value;
            InvokeAsync(StateHasChanged);
        }
    }
}
