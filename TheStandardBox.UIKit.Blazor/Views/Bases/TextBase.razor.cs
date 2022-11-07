// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class TextBase : BasicComponent
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public bool IsBold { get; set; }

        [Parameter]
        public bool IsItalic { get; set; }

        public void SetValue(string value)
        {
            this.Value = value;
            InvokeAsync(StateHasChanged);
        }
    }
}