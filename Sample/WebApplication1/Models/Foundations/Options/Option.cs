using System;
using System.Collections.Generic;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Models.Foundations.Options
{
    public class Option : IStandardEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}