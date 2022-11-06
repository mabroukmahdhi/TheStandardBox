using System;
using System.Collections.Generic;
using TheStandardBox.Core.Attributes.Annotations;
using TheStandardBox.Core.Attributes.Validations;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.Options;

namespace WebApplication1.Models.Foundations.Users
{
    public class User : IStandardEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [ValidateOnModify]
        public string Firstname { get; set; }
        [ValidateOnModify]
        public string Lastname { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public IEnumerable<Option> Options { get; set; }
    }
}