using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Controllers;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.UserOptions;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Models.Foundations.Options
{
    [GeneratedController("api/options",AllowedAction.GetAllEntities)]
    public class Option : IStandardEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        [JsonIgnore]
        public IEnumerable<UserOption> Users { get; set; }
    }
}