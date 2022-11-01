// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace TheStandardBox.Core.Models.Foundations.Standards.Exceptions
{
    public class NotFoundEntityException : Xeption
    {
        public NotFoundEntityException(string entityName, Guid orderId)
            : base(message: $"Couldn't find {entityName} with orderId: {orderId}.")
        { }
    }
}