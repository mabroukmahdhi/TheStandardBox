// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class EntityValidationException : Xeption
    {
        public EntityValidationException(string entityName, Xeption innerException)
            : base(message: $"{entityName} validation errors occurred, please try again.",
                  innerException)
        { }
    }
}