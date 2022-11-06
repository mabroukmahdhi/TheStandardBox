// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Bases.Exceptions
{
    public class NotFoundEntityException : Xeption
    {
        public NotFoundEntityException(string entityName, Guid entityId)
            : base(message: $"Couldn't find {entityName} with Id: {entityId}.")
        { }

        public NotFoundEntityException(string entityName, params object[] entityIds)
            : base(message: $"Couldn't find {entityName} with Ids: {string.Join("; ", entityIds)}.")
        { }
    }
}