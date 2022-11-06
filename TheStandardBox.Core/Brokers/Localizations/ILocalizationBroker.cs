// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Resources;

namespace TheStandardBox.Core.Brokers.Localizations
{
    public interface ILocalizationBroker
    {
        ResourceManager ResourceManager { get; }
        string GetText(string key);
        string this[string key] { get; }
    }
}
