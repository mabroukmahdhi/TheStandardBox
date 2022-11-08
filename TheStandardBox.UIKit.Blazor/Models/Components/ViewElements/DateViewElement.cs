// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace TheStandardBox.UIKit.Blazor.Models.Components.ViewElements
{
    public class DateViewElement : ViewElement<DateTimeOffset>
    {
        public override ViewElementType Type { get; set; } = ViewElementType.DatePicker;
    }
}
