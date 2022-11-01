// ---------------------------------------------------------------
// Copyright (c) mabrouk. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class InvalidEntityException : Xeption
    {
        public InvalidEntityException(string modelName)
            : base(message: $"Invalid {modelName}. Please correct the errors and try again.")
        { }
    }
}