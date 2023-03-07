// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases.Standards
{
    public partial class StdButton : StandardComponent
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public bool IsPrimary { get; set; } = true;

        [Parameter]
        public Action OnClick { get; set; }

        public void Click() => OnClick.Invoke();

        protected override RenderFragment CreateComponent() =>
            RenderingService.CreateStandardButton(this);
    }
}