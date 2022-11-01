// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class AlreadyExistsEntityException : Xeption
    {
        public AlreadyExistsEntityException(string entityName, Exception innerException)
            : base(message: $"{entityName} with the same Id already exists.", innerException)
        { }
    }
}