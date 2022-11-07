// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class TextBoxBase : InputComponent<string>
    {
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

        public void SetPlaceholder(string placeholder) =>
            this.Placeholder = placeholder;

        public void SetIsPassword(bool isPassword) =>
            this.IsPassword = isPassword;

        public void SetMaxLength(int length) =>
            this.MaxLength = length;

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

        private InputType GetInputType()
        {
            if (IsPassword)
                return InputType.Password;

            return InputType.Text;
        }

        public void OnInputEvent(InputEventArgs args)
        {
            OnInput?.Invoke();
        }
    }
}