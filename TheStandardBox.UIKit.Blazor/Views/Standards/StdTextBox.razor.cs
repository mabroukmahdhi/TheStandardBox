// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Standards
{
    public partial class StdTextBox : StandardComponent
    {
        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public Action<ChangeEventArgs> OnChanged { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public bool IsPassword { get; set; }

        [Parameter]
        public int MaxLength { get; set; } = 0;

        [Parameter]
        public string Width { get; set; }

        [Parameter]
        public Action OnInput { get; set; }

        public void Changed(ChangeEventArgs args) =>
            OnChanged?.Invoke(args);

        public void SetValue(string value)
        {
            this.Value = value;
            ValueChanged.InvokeAsync(this.Value);
        }

        public void SetPlaceholder(string placeholder) =>
            this.Placeholder = placeholder;

        public void SetIsPassword(bool isPassword) =>
            this.IsPassword = isPassword;

        public void SetMaxLength(int length) =>
            this.MaxLength = length;

        public void SetWidth(string width) =>
            this.Width = width;

        protected override Dictionary<string, object> GetAttributes()
        {
            if (MaxLength > 0)
            {
                this.Attributes ??= new Dictionary<string, object>();

                if (Attributes.ContainsKey("maxlength"))
                {
                    this.Attributes["maxlength"] = MaxLength;
                }
                else
                {
                    this.Attributes.Add("maxlength", MaxLength);
                }
            }

            return base.GetAttributes();
        }

        protected virtual async void OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = (string)changeEventArgs.Value;
            await ValueChanged.InvokeAsync(this.Value);
        }

        protected override RenderFragment CreateComponent() =>
            RenderingService.CreateStandardTextBox(this);
    }
}