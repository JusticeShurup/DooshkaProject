using BLL.UserLogic.Queries;
using Dooshka.Application.Features.UserFeatures.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Dooshka.API.Controllers
{
    [EnableCors("AllowAll")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : Controller
    {
        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ChangeName(ISender sender, [FromBody] ChangeNameRequest command)
        {
            var result = await sender.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ChangePassword(ISender sender, [FromBody] ChangePasswordRequest command)
        {
            await sender.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetCompletedTaskStatistic(ISender sender)
        {
            var result = await sender.Send(new GetCompletedTaskStatisticRequest());

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetIncompletedTaskStatistic(ISender sender)
        {
            var result = await sender.Send(new GetIncompletedTaskStatisticRequest());

            return Ok(result);
        }

    }
}
