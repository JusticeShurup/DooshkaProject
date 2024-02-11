using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs.Auths;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dooshka.API.Controllers
{

    /// <summary>
    /// Controller to work with auth service!
    /// </summary>
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Method to register!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request">
        ///     Request data
        /// </param>
        /// <returns></returns>

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), 201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> Register(ISender sender, RegisterRequest request)
        {
            UserDTO result = await sender.Send(request);

            return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Method to login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> Login(ISender sender, LoginRequest request)
        {
            UserDTO result = await sender.Send(request);

            return Ok(result);
        }
        /// <summary>
        /// Method to refresh your expired access token
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(RefreshResponseDTO), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> Refresh(ISender sender, RefreshAccessTokenRequest request)
        {
            RefreshResponseDTO result;

            result = await sender.Send(request);


            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Method to log out. 
        /// Make current access and refresh tokens unavailable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout(ISender sender, LogoutRequest request)
        {
            await sender.Send(request);

            return Ok();
        }
        /// <summary>
        /// Method for user.
        /// Called from a mail message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="email"></param>
        /// <param name="confirmationCode"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ConfirmEmail(ISender sender, [FromQuery][EmailAddress] string email, [FromQuery] int confirmationCode)
        {
            await sender.Send(new ConfirmEmailRequest() { Email = email.ToString(), Code = confirmationCode });

            return Ok("Успешно подтверждена почта");
        }

    }
}
