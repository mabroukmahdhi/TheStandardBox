// ---------------------------------------------------------------
// Copyright (c) mabrouk. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class NullEntityException : Xeption
    {
        public NullEntityException(string modelName)
            : base(message: $"{modelName} is null.")
        { }
    }
}