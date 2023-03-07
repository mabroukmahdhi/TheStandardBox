// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using TheStandardBox.Core.Models.Configurations;
using TheStandardBox.Core.Models.Foundations.EmailItems;

namespace TheStandardBox.Core.Brokers.Mailings
{
    public class MailingBroker : IMailingBroker
    {
        private readonly SmtpClient stmpClient;
        private readonly SmtpClientConfiguration stmpClientConfiguration;

        public MailingBroker(IConfiguration configuration)
        {
            var localConfig = configuration.Get<StandardConfigurations>();

            this.stmpClientConfiguration
                = localConfig.SmtpClientConfiguration;

            stmpClient = CreateStmpClient();
        }

        public void SendEmailAsync(EmailItem email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Content,
                IsBodyHtml = email.IsHtml,
            };

            foreach (var to in email.To)
            {
                mailMessage.To.Add(to);
            }

            foreach (var cc in email.CC)
            {
                mailMessage.CC.Add(cc);
            }

            foreach (var bcc in email.BCC)
            {
                mailMessage.Bcc.Add(bcc);
            }

            stmpClient.Send(mailMessage);
        }

        private SmtpClient CreateStmpClient()
        {
            SmtpClient client = new(this.stmpClientConfiguration.Server)
            {
                Port = this.stmpClientConfiguration.Port,

                Credentials = new NetworkCredential(
                   userName: this.stmpClientConfiguration.MasterEmail,
                   password: this.stmpClientConfiguration.MasterPassword),

                EnableSsl = this.stmpClientConfiguration.EnableSsl
            };

            return client;
        }
    }

}
