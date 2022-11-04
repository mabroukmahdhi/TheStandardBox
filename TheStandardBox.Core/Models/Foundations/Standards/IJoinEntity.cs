// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace TheStandardBox.Core.Models.Foundations.Standards
{
    public interface IJoinEntity : IEntity
    {
        IEnumerable<Guid> Ids { get; set; }
    }
}
