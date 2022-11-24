// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Views.Standards;

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

        public virtual RenderFragment CreateStandardLabel(StdLabel label) =>
            builder =>
            {
                builder.OpenElement(0, "label");
                builder.AddContent(3, label.Value);
                builder.CloseElement();
            };
    }
}