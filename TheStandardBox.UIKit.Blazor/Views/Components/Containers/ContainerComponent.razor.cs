// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Models.Components.Containers;
using TheStandardBox.UIKit.Blazor.Views.Standards;

namespace TheStandardBox.UIKit.Blazor.Views.Components.Containers
{
    public partial class ContainerComponent : StandardComponent
    {
        [Parameter]
        public ComponentState State { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public string Error { get; set; }
    }
}
