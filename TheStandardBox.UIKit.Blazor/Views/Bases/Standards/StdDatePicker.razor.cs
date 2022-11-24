// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases.Standards
{
    public partial class StdDatePicker : StandardComponent
    {
        [Parameter]
        public DateTime Value { get; set; }

        [Parameter]
        public EventCallback<DateTime> ValueChanged { get; set; }

        public void SetValue(DateTime value)
        {
            Value = value;
            ValueChanged.InvokeAsync(Value);
        }

        public string GetFormattedValue(string format) =>
            Value.ToString(format);

        protected virtual async void OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            Value = (DateTime)changeEventArgs.Value;
            await ValueChanged.InvokeAsync(Value);
        }

        protected override RenderFragment CreateComponent() =>
            RenderingService.CreateStandardDatePicker(this);
    }
}