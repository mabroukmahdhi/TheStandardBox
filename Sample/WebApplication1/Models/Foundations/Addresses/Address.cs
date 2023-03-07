using System;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace WebApplication1.Models.Foundations.Addresses
{
    [GeneratedController("api/addresses")]
    public class Address : IStandardEntity
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Extra { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}