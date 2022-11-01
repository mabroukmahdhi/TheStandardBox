// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class LockedEntityException : Xeption
    {
        public LockedEntityException(string modelName, Exception innerException)
            : base(message: $"Locked {modelName} record exception, please try again later", innerException)
        {
        }
    }
}