using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TheStandardBox.Core.Attributes.Annotations;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Attributes.Validations;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.UserOptions;

namespace WebApplication1.Models.Foundations.Users
{
    [GeneratedController("api/users")]
    public class User : IStandardEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [ValidateOnModify]
        public string Firstname { get; set; }
        [ValidateOnModify]
        public string Lastname { get; set; }

        public Guid AddressId { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        [JsonIgnore]
        public IEnumerable<UserOption> Options { get; set; }
    }
}