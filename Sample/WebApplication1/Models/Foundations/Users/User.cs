using System;
using System.Collections.Generic;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.Options;

namespace WebApplication1.Models.Foundations.Users
{
    public class User : IStandardEntity
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}