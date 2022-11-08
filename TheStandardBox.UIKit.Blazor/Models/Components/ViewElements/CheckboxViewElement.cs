// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace TheStandardBox.UIKit.Blazor.Models.Components.ViewElements
{
    public class CheckboxViewElement : ViewElement<bool>
    {
        public override ViewElementType Type { get; set; } = ViewElementType.ChecBox;
    }
}