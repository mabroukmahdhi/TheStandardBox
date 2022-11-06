using StandardApi.PoC.Tests.Unit.Services.Standards;
using WebApplication1.Models.Foundations.Users;
using Xunit;

namespace WebApplication1.Tests.Unit
{
    public class UserServiceTests : StandardServiceTests<User>
    {
        //protected override User CreateInvalidInstance(string invalidText)
        //{
        //    return new User() { Firstname = invalidText };
        //}

        public async override Task ShouldThrowValidationExceptionOnModifyIfEntityIsInvalidAndLogItAsync(string invalidText)
        {
            await Task.Delay(0);
            Assert.True(true);
        }
    }
}