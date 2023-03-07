using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Controllers;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.UserOptions;

namespace WebApplication1.Models.Foundations.Options
{
    [GeneratedController("api/options",
        anonymousActions: new AllowedAction[] { AllowedAction.PostEntity })]
    [Authorize]
    public class Option : IStandardEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        [JsonIgnore]
        public IEnumerable<UserOption> Users { get; set; }
    }
}