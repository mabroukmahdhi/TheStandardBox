// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class InvalidEntityReferenceException : Xeption
    {
        public InvalidEntityReferenceException(string modelName, Exception innerException)
            : base(message: $"Invalid {modelName} reference error occurred.", innerException) { }
    }
}