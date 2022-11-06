using System;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Foundations.Joins;
using WebApplication1.Models.Foundations.Options;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Models.Foundations.UserOptions
{
    [GeneratedController("api/useroptions")]
    public class UserOption : IJoinEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid OptionId { get; set; }
        public Option Option { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}