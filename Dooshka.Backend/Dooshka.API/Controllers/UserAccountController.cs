using Dooshka.Application.Features.DTOs;
using Dooshka.Application.Features.DTOs.ToDoItems;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(typeof(UserAccountDTO), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(EmptyResult), 401)]
        public async Task<IActionResult> ChangeName(ISender sender, [FromBody] ChangeNameRequest command)
        {
            var result = await sender.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(EmptyResult), 401)]
        public async Task<IActionResult> ChangePassword(ISender sender, [FromBody] ChangePasswordRequest command)
        {
            await sender.Send(command);

            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<CreatedToDoItemDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(EmptyResult), 401)]
        public async Task<IActionResult> GetCompletedTaskStatistic(ISender sender)
        {
            var result = await sender.Send(new GetCompletedTaskStatisticRequest());

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>
        /// 100
        /// </returns>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(List<CreatedToDoItemDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(EmptyResult), 401)]
        public async Task<IActionResult> GetIncompletedTaskStatistic(ISender sender)
        {
            var result = await sender.Send(new GetIncompletedTaskStatisticRequest());

            return Ok(result);
        }

    }
}
