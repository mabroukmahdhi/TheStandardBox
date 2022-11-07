// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace TheStandardBox.UIKit.Blazor.Models.Components.Toasts
{
    public class ToastStyle
    {
        private ToastStyle(string value) =>
            this.value = value;

        public static ToastStyle Success => new("success");
        public static ToastStyle Danger => new("danger");
        public static ToastStyle Warning => new("warning");
        public static ToastStyle Info => new("info");

        private readonly string value;

        public static implicit operator string(ToastStyle t) => t.value;
        public static explicit operator ToastStyle(string s) => new(s);

        public override string ToString() => value;
    }
}
