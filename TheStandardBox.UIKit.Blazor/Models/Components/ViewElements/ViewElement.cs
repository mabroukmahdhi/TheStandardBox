// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace TheStandardBox.UIKit.Blazor.Models.Components.ViewElements
{
    public  class ViewElement<TValue> : IViewElement
    {
        public string Placeholder { get; set; }
        public TValue Value { get; set; }
        public virtual ViewElementType Type { get; set; }

        public string Id { get; set; }

        public object GetValue() => Value;
    }
}
