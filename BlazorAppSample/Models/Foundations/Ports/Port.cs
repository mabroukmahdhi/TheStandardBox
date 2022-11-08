using System;
using TheStandardBox.Core.Models.Foundations.Standards;
using TheStandardBox.UIKit.Blazor.Attributes;

namespace BlazorAppSample.Models.Foundations.Ports
{
    public class Port : IStandardEntity
    {
        public Guid Id { get; set; }
        [Field]
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        [Field]
        public string Information { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
