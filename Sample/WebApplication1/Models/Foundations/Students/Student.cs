using System;
using System.Collections.Generic;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.StudentCards;

namespace WebApplication1.Models.Foundations.Students
{
    [GeneratedController("api/students")]
    public class Student : IStandardEntity
    {
        public Guid Id { get ; set ; }
        public string Name { get ; set ; }
        public DateTimeOffset CreatedDate { get ; set ; }
        public DateTimeOffset UpdatedDate { get ; set ; }
        public IEnumerable<StudentCard> Cards { get; set; }
    }
}
