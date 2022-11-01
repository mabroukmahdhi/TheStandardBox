// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class EntityServiceException : Xeption
    {
        public EntityServiceException(string modelName, Exception innerException)
            : base(message: $"{modelName} service error occurred, contact support.", innerException)
        { }
    }
}