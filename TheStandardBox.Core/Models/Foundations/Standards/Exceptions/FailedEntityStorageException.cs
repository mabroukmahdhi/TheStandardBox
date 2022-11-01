// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class FailedEntityStorageException : Xeption
    {
        public FailedEntityStorageException(string modelName, Exception innerException)
            : base(message: $"Failed {modelName} storage error occurred, contact support.", innerException)
        { }
    }
}