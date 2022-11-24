// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Standards
{
    public partial class StdCheckBox : StandardComponent
    {
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public Action OnInput { get; set; }

        public void SetValue(bool value)
        {
            this.Value = value;
            ValueChanged.InvokeAsync(this.Value);
        }

        protected virtual async void OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = (bool)changeEventArgs.Value;
            await ValueChanged.InvokeAsync(this.Value);
        }

        protected override RenderFragment CreateComponent() =>
            RenderingService.CreateStandardCheckBox(this);
    }
}