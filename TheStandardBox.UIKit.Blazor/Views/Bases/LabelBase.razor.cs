// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Models.Components.Labels;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class LabelBase : TextBase
    {
        [Parameter]
        public LabelTextColor TextColor { get; set; } = LabelTextColor.Dark;

        protected string textColorClass =>
            $"text-{TextColor}".ToLower();

        protected override string GetCssClass() =>
            $"{CssClass} {textColorClass}";

        public void SetLabelTextColor(LabelTextColor textColor) =>
            TextColor = textColor;
    }
}