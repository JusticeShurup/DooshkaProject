using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs;
using Dooshka.Application.Features.DTOs.Auths;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dooshka.API.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register(ISender sender, RegisterRequest request)
        {
            UserDTO result = await sender.Send(request);

            return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login(ISender sender, LoginRequest request)
        {
            UserDTO result = await sender.Send(request);

            return Ok(result);
        }

        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Refresh(ISender sender, RefreshAccessTokenRequest request)
        {
            RefreshResponseDTO result;

            result = await sender.Send(request);


            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }

        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Logout(ISender sender, LogoutRequest request)
        {
            await sender.Send(request);

            return Ok();
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(ISender sender, [FromQuery][EmailAddress] string email, [FromQuery] int confirmationCode)
        {
            await sender.Send(new ConfirmEmailRequest() { Email = email.ToString(), Code = confirmationCode });

            return Ok("Успешно подтверждена почта");
        }

    }
}
