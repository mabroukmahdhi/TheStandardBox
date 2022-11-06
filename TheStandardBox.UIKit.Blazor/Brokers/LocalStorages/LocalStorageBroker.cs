// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

namespace TheStandardBox.UIKit.Blazor.Brokers.LocalStorages
{
    public class LocalStorageBroker : ILocalStorageBroker
    {
        protected readonly ILocalStorageService localStorageService;
        protected readonly ISessionStorageService sessionStorageService;

        public LocalStorageBroker(
            ILocalStorageService localStorageService,
            ISessionStorageService sessionStorageService)
        {
            this.localStorageService = localStorageService;
            this.sessionStorageService = sessionStorageService;
        }

        public virtual async ValueTask<TValue> InsertValueAsync<TValue>(string key, TValue value)
        {
            await localStorageService.SetItemAsync(key, value);
            return value;
        }

        public virtual async ValueTask<TValue> FindValueAsync<TValue>(string key) =>
            await localStorageService.GetItemAsync<TValue>(key);

        public virtual async ValueTask<TValue> UpdateValueAsync<TValue>(string key, TValue value)
        {
            await DeleteValueAsync<TValue>(key);

            return await InsertValueAsync(key, value);
        }

        public virtual async ValueTask DeleteValueAsync<TValue>(string key) =>
            await localStorageService.RemoveItemAsync(key);
    }
}