// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TheStandardBox.Core.Models.Foundations.EmailItems
{
    public class EmailItem
    {
        public EmailItem()
        {
            To = new Collection<string>();
            CC = new Collection<string>();
            BCC = new Collection<string>();
        }
        public string From { get; set; }
        public ICollection<string> To { get; set; }
        public ICollection<string> CC { get; set; }
        public ICollection<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; set; }
    }
}