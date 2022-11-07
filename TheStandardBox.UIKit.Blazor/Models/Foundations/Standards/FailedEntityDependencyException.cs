// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.UIKit.Blazor.Models.Foundations.Standards
{
    public class FailedEntityDependencyException : Xeption
    {
        public FailedEntityDependencyException(string modelName, Exception innerException)
            : base(message: $"Failed {modelName} dependency error occurred, contact support.",
                  innerException)
        { }
    }
}
