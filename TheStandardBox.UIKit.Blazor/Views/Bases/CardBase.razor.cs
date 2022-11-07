// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.UIKit.Blazor.Models.Components.Cards;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class CardBase : BasicComponent
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string SubTitle { get; set; }

        [Parameter]
        public string Image { get; set; }

        [Parameter]
        public bool HasShadow { get; set; } = true;

        [Parameter]
        public string TextContent { get; set; }

        [Parameter]
        public HeaderAlign HeaderAlign { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public RenderFragment Footer { get; set; }

        protected bool HasTitles =>
            !string.IsNullOrEmpty(Title)
            && !string.IsNullOrEmpty(SubTitle);


        private string GetHeaderAlign()
        {
            return HeaderAlign == HeaderAlign.Default ?
                null : HeaderAlign.ToString();
        }

        protected override string GetStyle() =>
            $"{(string.IsNullOrWhiteSpace(Style) ? "" : $"{Style};")}" +
            $"{(HasShadow ? string.Empty : "box-shadow:none;")}";
    }
}
