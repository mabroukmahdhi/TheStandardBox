using StandardApi.PoC.Tests.Unit.Services.Standards;
using TheStandardBox.Core.Models.Foundations.Bases.Exceptions;
using WebApplication1.Models.Foundations.Companies;

namespace WebApplication1.Tests.Unit
{
    public class CompanyServiceTests : StandardServiceTests<Company>
    {
        protected override InvalidEntityException CreateInvalidEntityExceptionOnAdd()
        {
            return base.CreateInvalidEntityExceptionOnAdd();
        }

        public override Task ShouldThrowValidationExceptionOnAddIfEntityIsInvalidAndLogItAsync(string invalidText)
        {
            return base.ShouldThrowValidationExceptionOnAddIfEntityIsInvalidAndLogItAsync(invalidText);
        }

    }
}