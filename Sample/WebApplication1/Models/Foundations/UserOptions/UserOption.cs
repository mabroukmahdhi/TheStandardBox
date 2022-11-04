using System;
using TheStandardBox.Core.Attributes.Annotations;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.Options;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Models.Foundations.UserOptions
{
    public class UserOption : IStandardEntity
    {
        [PrimaryKey]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [PrimaryKey]
        public Guid OptionId { get; set; }
        public Option Option { get; set; } 
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
