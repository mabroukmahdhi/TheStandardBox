// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace TheStandardBox.UIKit.Blazor.Models.Components.ViewElements
{
    public interface IViewElement
    {
        ViewElementType Type { get; }
        string Id { get; set; }
        object GetValue();
    }
}