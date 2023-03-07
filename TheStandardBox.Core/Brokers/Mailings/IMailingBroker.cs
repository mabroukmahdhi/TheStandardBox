// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using TheStandardBox.Core.Models.Foundations.EmailItems;

namespace TheStandardBox.Core.Brokers.Mailings
{
    public interface IMailingBroker
    {
        void SendEmailAsync(EmailItem email);
    }
}
