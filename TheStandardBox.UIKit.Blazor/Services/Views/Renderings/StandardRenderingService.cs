// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Views.Bases.Standards;

namespace TheStandardBox.UIKit.Blazor.Services.Views.Renderings
{
    public class StandardRenderingService : IStandardRenderingService
    {
        public virtual RenderFragment CreateStandardButton(StdButton button) =>
            builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddAttribute(3, "onclick", button.OnClick);
                builder.AddContent(4, button.Label);
                builder.CloseElement();
            };

        public RenderFragment CreateStandardCheckBox(StdCheckBox checkBox) =>
            builder =>
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(3, "type", "checkbox");
                builder.AddAttribute(3, "value", checkBox.Value);
                builder.AddAttribute(4, "onchange",
                    EventCallback.Factory.CreateBinder(
                        receiver: checkBox,
                        setter: _value => checkBox.SetValue(_value),
                        existingValue: checkBox.Value));

                builder.CloseElement();
            };

        public RenderFragment CreateStandardDatePicker(StdDatePicker datePicker) =>
            builder =>
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(3, "type", "date");
                builder.AddAttribute(4, "value", datePicker.GetFormattedValue("yyyy-MM-dd"));
                builder.AddAttribute(5, "onchange",
                    EventCallback.Factory.CreateBinder(
                        receiver: datePicker,
                        setter: _value => datePicker.SetValue(_value),
                        existingValue: datePicker.Value));

                builder.CloseElement();
            };

        public virtual RenderFragment CreateStandardLabel(StdLabel label) =>
            builder =>
            {
                builder.OpenElement(0, "label");
                builder.AddContent(3, label.Value);
                builder.CloseElement();
            };

        public RenderFragment CreateStandardTextBox(StdTextBox textBox) =>
            builder =>
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(3, "type", textBox.IsPassword ? "password" : "text");
                builder.AddAttribute(4, "value", textBox.Value);
                builder.AddAttribute(5, "onchange",
                    EventCallback.Factory.CreateBinder(
                        receiver: textBox,
                        setter: _value => textBox.SetValue(_value),
                        existingValue: textBox.Value));

                builder.CloseElement();
            };
    }
}