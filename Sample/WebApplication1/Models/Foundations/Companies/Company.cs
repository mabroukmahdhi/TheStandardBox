using System;
using TheStandardBox.Core.Attributes.Contollers;
using TheStandardBox.Core.Models.Foundations.Standards;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Models.Foundations.Companies
{
    [GeneratedController("api/companies")]
    public class Company : IStandardEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ResponsableName { get; set; }
        public string ResponsableLastName { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public bool HasOnlyOneAddress { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public string Description { get; set; }
        public string VATNumber { get; set; }
        public bool IsInsured { get; set; }
        public DateTimeOffset InsuranceExpiryDate { get; set; }
        public double InsuranceAmount { get; set; }
        public bool AcceptCheck { get; set; }
        public bool AcceptCash { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
