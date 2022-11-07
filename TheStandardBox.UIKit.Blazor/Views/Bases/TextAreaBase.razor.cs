﻿// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class TextAreaBase : ComponentBase
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public string Height { get; set; }

        public bool IsEnabled => IsDisabled is false;

        private Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = changeEventArgs.Value.ToString();

            return ValueChanged.InvokeAsync(this.Value);
        }

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }

        public Task SetValueAsync(string value) =>
        InvokeAsync(async () =>
        {
            this.Value = value;
            await this.ValueChanged.InvokeAsync(this.Value);
        });
    }
}
