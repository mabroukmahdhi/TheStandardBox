// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Models.Components.Containers;

namespace TheStandardBox.UIKit.Blazor.Views.Components.Containers
{
    public partial class ContainerStatesComponent : ComponentBase
    {
        [Parameter]
        public ComponentState State { get; set; }

        [Parameter]
        public RenderFragment LoadingFragment { get; set; }

        [Parameter]
        public RenderFragment ContentFragment { get; set; }

        [Parameter]
        public RenderFragment ErrorFragment { get; set; }

        private RenderFragment GetComponentStateFragment()
        => State switch
        {
            ComponentState.Loading => LoadingFragment,
            ComponentState.Content => ContentFragment,
            ComponentState.Error => ErrorFragment,
            _ => ErrorFragment
        };
    }
}
