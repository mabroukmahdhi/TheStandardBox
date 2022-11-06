// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading.Tasks;

namespace TheStandardBox.UIKit.Blazor.Brokers.LocalStorages
{
    public interface ILocalStorageBroker
    {
        ValueTask<TValue> InsertValueAsync<TValue>(string key, TValue value);
        ValueTask<TValue> FindValueAsync<TValue>(string key);
        ValueTask<TValue> UpdateValueAsync<TValue>(string key, TValue value);
        ValueTask DeleteValueAsync<TValue>(string key);
    }
}