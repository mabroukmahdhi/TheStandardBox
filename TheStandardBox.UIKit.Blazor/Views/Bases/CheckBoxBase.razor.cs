// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class CheckBoxBase : InputComponent<bool>
    {
        [Parameter]
        public string Label { get; set; }


        public void SetLabel(string label) =>
            this.Label = label;

        public bool IsChecked => this.Value;
    }
}