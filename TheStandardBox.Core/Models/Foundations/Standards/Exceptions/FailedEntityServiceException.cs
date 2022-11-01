// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class FailedEntityServiceException : Xeption
    {
        public FailedEntityServiceException(string entityName, Exception innerException)
            : base(message: $"Failed {entityName} service occurred, please contact support", innerException)
        { }
    }
}