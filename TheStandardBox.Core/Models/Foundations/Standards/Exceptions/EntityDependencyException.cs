// ---------------------------------------------------------------
// Copyright (c) mabrouk. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class EntityDependencyException : Xeption
    {
        public EntityDependencyException(string modelName, Xeption innerException) :
            base(message: $"{modelName} dependency error occurred, contact support.", innerException)
        { }
    }
}