// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

namespace TheStandardBox.Core.Models.Configurations
{
    public class SmtpClientConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string MasterEmail { get; set; }
        public string MasterPassword { get; set; }
        public bool EnableSsl { get; set; }
    }
}
