// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Globalization;
using System.Resources;
using TheStandardBox.Core.Brokers.Localizations;
using TheStandardBox.UIKit.Blazor.Resources;

namespace TheStandardBox.UIKit.Blazor.Brokers.Localizations
{
    public class UILocalizationBroker : ILocalizationBroker
    {
        public string this[string key] => GetText(key);

        public ResourceManager ResourceManager =>
            SResource.ResourceManager;

        public string GetText(string key)
        {
            string value =
                ResourceManager.GetString(
                    name: key,
                    culture: CultureInfo.CurrentCulture);

            return value;
        }
    }
}
