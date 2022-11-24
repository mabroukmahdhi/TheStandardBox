// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Services.Views.Renderings;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public class BasicComponent : ComponentBase
    {
        [Inject]
        public IRenderingService RenderingService { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

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

        protected virtual RenderFragment CreateComponent() =>
            builder => { };
    }
}