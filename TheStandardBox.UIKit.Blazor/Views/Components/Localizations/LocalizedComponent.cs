// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using TheStandardBox.Core.Brokers.Localizations;
using TheStandardBox.UIKit.Blazor.Views.Components.Containers;

namespace TheStandardBox.UIKit.Blazor.Views.Components.Localizations
{
    public class LocalizedComponent : ContainerComponent
    {
        [Inject]
        public ILocalizationBroker Localizer { get; set; }
    }
}