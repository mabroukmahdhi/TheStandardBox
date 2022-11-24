// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class StdLabel : BasicComponent
    {
        [Parameter]
        public string Value { get; set; }

        protected override RenderFragment CreateComponent() =>
            RenderingService.CreateStandardLabel(this);
    }
}