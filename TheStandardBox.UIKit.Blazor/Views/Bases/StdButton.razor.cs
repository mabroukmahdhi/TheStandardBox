// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class StdButton : BasicComponent
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        public void Click() => OnClick.Invoke();

        public override RenderFragment CreateComponent() => builder =>
        {
            builder.OpenElement(0, "button");
            builder.AddContent(1, Label);
            builder.AddAttribute(2, "onclick", Click);
            builder.CloseElement();
        };
    }
}