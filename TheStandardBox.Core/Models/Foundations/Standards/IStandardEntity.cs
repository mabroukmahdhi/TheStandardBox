// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using TheStandardBox.Core.Models.Foundations.Bases;

namespace TheStandardBox.Core.Models.Foundations.Standards
{
    public interface IStandardEntity : IBaseEntity
    {
        Guid Id { get; set; }
    }
}