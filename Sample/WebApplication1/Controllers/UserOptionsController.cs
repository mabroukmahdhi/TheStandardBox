using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using TheStandardBox.Data.Controllers;
using TheStandardBox.Data.Services.Foundations.Standards;
using WebApplication1.Models.Foundations.UserOptions;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserOptionsController : StandardController<UserOption>
    {
        public UserOptionsController(IStandardService<UserOption> standardService)
            : base(standardService)
        { }

        [HttpGet("{userId}/{optionId}")]
        public virtual ValueTask<ActionResult<UserOption>> GetEntityByIdAsync(Guid userId, Guid optionId) =>
             TryCatchOnGetById(async () =>
             {
                 return await this.standardService.RetrieveEntityByIdAsync(userId, optionId);
             });

    }
}