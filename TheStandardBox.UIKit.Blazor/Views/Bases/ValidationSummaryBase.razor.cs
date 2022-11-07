// ---------------------------------------------------------------
// Copyright (c) Cydista. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TheStandardBox.UIKit.Blazor.Views.Bases
{
    public partial class ValidationSummaryBase : ComponentBase
    {
        [Parameter]
        public IDictionary ValidationData { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public string Key { get; set; }

        [Parameter]
        public string Color { get; set; }

        public IEnumerable<string> Errors
        {
            get => this.ValidationData?[Key] as IEnumerable<string>;
            set => Errors = value;
        }
    }
}
