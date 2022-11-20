// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Services.Views.Renderings;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class StdButton : BasicComponent
    {
        [Inject]
        public IRenderingService RenderingService { get; set; }
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        public void Click() => OnClick.Invoke();

        private RenderFragment CreateComponent() =>
            RenderingService.CreateStandardButton(this);
    }
}