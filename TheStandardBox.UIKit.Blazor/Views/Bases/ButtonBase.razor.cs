// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class ButtonBase : BasicComponent
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string FaIcon { get; set; }

        [Parameter]
        public string BorderColor { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        public void Click() =>
             OnClick?.Invoke();

        protected override string GetCssClass()
        {
            if (string.IsNullOrWhiteSpace(CssClass))
            {
                return "e-btn e-outline";
            }
            return CssClass;
        }

        private string GetIcon()
        {
            if (string.IsNullOrWhiteSpace(FaIcon))
                return string.Empty;

            return $"far fa-{FaIcon}";
        }
    }
}