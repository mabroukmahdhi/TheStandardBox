using System;
using System.ComponentModel.DataAnnotations;
using TheStandardBox.Core.Models.Foundations.Standards;

namespace BlazorAppSample.Models.Foundations.Ports
{
    public class Port : IStandardEntity
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Port name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        [Required]
        [Display(Name = "Port details")]
        [DataType(DataType.Text)]
        public string Information { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
