// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;
using TheStandardBox.UIKit.Blazor.Models.Components.Toasts;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class MessageBase : ComponentBase
    {
        SfToast ToastObj;

        [Parameter]
        public ToastViewPosition Position { get; set; } = ToastViewPosition.TopCenter;

        [Parameter]
        public string ToastId { get; set; } = Guid.NewGuid().ToString();

        [Parameter]
        public string ToastTitle { get; set; } = "Notification Toast";

        [Parameter]
        public string Style { get; set; } = ToastStyle.Success;

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string ToastContent { get; set; } = string.Empty;

        [Parameter]
        public int ToastTimeout { get; set; } = 0;

        public async Task Show()
        {
            CssClass = $"e-toast-{Style}";
            Icon = $"e-{Style} toast-icons";
            this.StateHasChanged();
            await this.ToastObj.ShowAsync();
        }

        public void Hide(object element = null) =>
            this.ToastObj.HideAsync("All");


        private string GetPositionX() => Position switch
        {
            ToastViewPosition.TopLeft => "Left",
            ToastViewPosition.TopRight => "Right",
            ToastViewPosition.TopCenter => "Center",
            ToastViewPosition.TopFullWidth => "Center",
            ToastViewPosition.BottomLeft => "Left",
            ToastViewPosition.BottomRight => "Right",
            ToastViewPosition.BottomCenter => "Center",
            ToastViewPosition.BottomFullWidth => "Center",
            _ => "Center",
        };

        private string GetPositionY() => Position switch
        {
            ToastViewPosition.TopLeft or
            ToastViewPosition.TopRight or
            ToastViewPosition.TopCenter or
            ToastViewPosition.TopFullWidth => "Top",
            ToastViewPosition.BottomLeft or
            ToastViewPosition.BottomRight or
            ToastViewPosition.BottomCenter or
            ToastViewPosition.BottomFullWidth => "Bottom",
            _ => "Top",
        };

        private string GetWidth() => Position switch
        {
            ToastViewPosition.TopLeft or
            ToastViewPosition.TopRight or
            ToastViewPosition.TopCenter or
            ToastViewPosition.BottomLeft or
            ToastViewPosition.BottomRight or
            ToastViewPosition.BottomCenter => "auto",
            ToastViewPosition.BottomFullWidth or
            ToastViewPosition.TopFullWidth => "100%",
            _ => "auto",
        };
    }
}
