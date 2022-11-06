using System;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Foundations.Joins;
using WebApplication1.Models.Foundations.Cards;
using WebApplication1.Models.Foundations.Students;

namespace WebApplication1.Models.Foundations.StudentCards
{
    [GeneratedController("api/studentcards")]
    public class StudentCard : IJoinEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public Guid CardId { get; set; }
        public Card Card { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
