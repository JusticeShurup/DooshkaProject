using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs.Auths;
using Dooshka.Application.Services;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Dooshka.Application.Features.AuthFeatures.Handlers.Commands
{
    public class RefreshAccessTokenRequestHandler : IRequestHandler<RefreshAccessTokenRequest, RefreshResponseDTO>
    {
        private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContext;


        public RefreshAccessTokenRequestHandler(TokenService tokenService, IHttpContextAccessor httpContext)
        {
            _tokenService = tokenService;
            _httpContext = httpContext;
        }


        public async Task<RefreshResponseDTO> Handle(RefreshAccessTokenRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContext.HttpContext.Items["User"];

            if (user.RefreshToken != _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last())
            {
                throw new BadRequestException("You need to login again");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

            var response = new RefreshResponseDTO { AccessToken = _tokenService.CreateJwtAccessToken(claims) };

            return response;
        }
    }
}
