// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace TheStandardBox.Core.Models.Controllers
{
    [Flags]
    public enum Action
    {
        None = 0,
        GetAllEntities = 1,
        GetEntityById = 2,
        PostEntity = 3,
        PutEntity = 4,
        DeleteEntityById = 5,
        DONTUSE = 6,
    }
}
