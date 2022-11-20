// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Views.Bases;

namespace TheStandardBox.UIKit.Blazor.Services.Views.Renderings
{
    public class RenderingService : IRenderingService
    {
        public virtual RenderFragment CreateStandardButton(StdButton button) =>
            builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(3, "onclick", button.OnClick);
                builder.AddContent(4, button.Label);
                builder.CloseElement();
            };
    }
}