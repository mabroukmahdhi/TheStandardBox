// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class EntityDependencyValidationException : Xeption
    {
        public EntityDependencyValidationException(string modelName, Xeption innerException)
            : base(message: $"{modelName} dependency validation occurred, please try again.", innerException)
        { }
    }
}