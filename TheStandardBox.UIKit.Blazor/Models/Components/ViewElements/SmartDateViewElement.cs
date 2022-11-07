// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace TheStandardBox.UIKit.Blazor.Models.Components.ViewElements
{
    public class SmartDateViewElement : SmartViewElement<DateTimeOffset>
    {
        public override SmartViewElementType Type => SmartViewElementType.DatePicker;
    }
}
