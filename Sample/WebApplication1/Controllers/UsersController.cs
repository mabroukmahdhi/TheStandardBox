using Microsoft.AspNetCore.Mvc;
using TheStandardBox.Data.Controllers;
using TheStandardBox.Data.Services.Foundations.Standards;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : StandardController<User>
    {
        public UsersController(IStandardService<User> standardService)
            : base(standardService)
        { }


    }
}