// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases.Standards
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
            Value = value;
            ValueChanged.InvokeAsync(Value);
        }

        public void SetPlaceholder(string placeholder) =>
            Placeholder = placeholder;

        public void SetIsPassword(bool isPassword) =>
            IsPassword = isPassword;

        public void SetMaxLength(int length) =>
            MaxLength = length;

        public void SetWidth(string width) =>
            Width = width;

        protected override Dictionary<string, object> GetAttributes()
        {
            if (MaxLength > 0)
            {
                Attributes ??= new Dictionary<string, object>();

                if (Attributes.ContainsKey("maxlength"))
                {
                    Attributes["maxlength"] = MaxLength;
                }
                else
                {
                    Attributes.Add("maxlength", MaxLength);
                }
            }

            return base.GetAttributes();
        }

        protected virtual async void OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            Value = (string)changeEventArgs.Value;
            await ValueChanged.InvokeAsync(Value);
        }

        protected override RenderFragment CreateComponent() =>
            RenderingService.CreateStandardTextBox(this);
    }
}