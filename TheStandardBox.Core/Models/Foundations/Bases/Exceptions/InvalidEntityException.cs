// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Bases.Exceptions
{
    public class InvalidEntityException : Xeption
    {
        public InvalidEntityException(string entityName)
            : base(message: $"Invalid {entityName}. Please correct the errors and try again.")
        { }

        public InvalidEntityException(string modelName, Exception innerException, IDictionary data)
            : base(message: $"Invalid {modelName} error occurred. Please correct the errors and try again.",
                  innerException,
                  data)
        { }
    }
}