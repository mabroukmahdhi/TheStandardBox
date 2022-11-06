// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Bases.Exceptions
{
    public class LockedEntityException : Xeption
    {
        public LockedEntityException(string entityName, Exception innerException)
            : base(message: $"Locked {entityName} record exception, please try again later", innerException)
        {
        }
    }
}