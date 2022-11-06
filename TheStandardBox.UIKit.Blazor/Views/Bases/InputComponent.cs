// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public class InputComponent<TInput> : BasicComponent
    {
        [Parameter]
        public TInput Value { get; set; }

        [Parameter]
        public EventCallback<TInput> ValueChanged { get; set; }

        public async Task SetValueAsync(TInput value)
        {
            this.Value = value;
            await ValueChanged.InvokeAsync(this.Value);
        }

        protected virtual async void OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = (TInput)changeEventArgs.Value;

            await ValueChanged.InvokeAsync(this.Value);
        }
    }
}
