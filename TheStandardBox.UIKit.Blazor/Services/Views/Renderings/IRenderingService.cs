// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Views.Standards;

namespace TheStandardBox.UIKit.Blazor.Services.Views.Renderings
{
    public interface IRenderingService
    {
        RenderFragment CreateStandardButton(StdButton button);
        RenderFragment CreateStandardLabel(StdLabel label);
    }
}