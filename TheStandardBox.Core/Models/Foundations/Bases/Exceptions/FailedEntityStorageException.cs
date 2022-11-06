// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Bases.Exceptions
{
    public class FailedEntityStorageException : Xeption
    {
        public FailedEntityStorageException(string entityName, Exception innerException)
            : base(message: $"Failed {entityName} storage error occurred, contact support.", innerException)
        { }
    }
}