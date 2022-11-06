using System;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace WebApplication1.Models.Foundations.Cards
{
    [GeneratedController("api/cards")]
    public class Card : IStandardEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
