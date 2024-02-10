using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs.Auths;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Application.Services;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dooshka.Application.Features.AuthFeatures.Handlers.Commands
{
    public class RefreshAccessTokenRequestHandler : IRequestHandler<RefreshAccessTokenRequest, RefreshResponseDTO>
    {
        private readonly TokenService _tokenService;
        private readonly IRepository<RevokedToken> _revokedTokenRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContext;


        public RefreshAccessTokenRequestHandler(TokenService tokenService, IHttpContextAccessor httpContext, IRepository<User> userRepository, IRepository<RevokedToken> revokedTokenRepository)
        {
            _tokenService = tokenService;
            _httpContext = httpContext;
            _userRepository = userRepository;
            _revokedTokenRepository = revokedTokenRepository;
        }


        public async Task<RefreshResponseDTO> Handle(RefreshAccessTokenRequest request, CancellationToken cancellationToken)
        {
            
            JwtSecurityToken? token = new JwtSecurityToken(request.RefreshToken);

            var result = _revokedTokenRepository.Find(x => x.Token == request.RefreshToken);

            if (result != null)
            {
                throw new UnauthorizedException("Token revoked");
            }

            string? email = token.Payload.Claims.FirstOrDefault(x => x.Type.ToString() == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                throw new UnauthorizedException("Logical error");
            }

            User? user = _userRepository.Find(x => x.Email == email);

            if (user == null)
            {
                throw new UnauthorizedException("User didn't not found");
            }

            if (!user.IsEmailConfirmed)
            {
                throw new UnauthorizedException("Email isn't confirmed");
            }

            if (user.RefreshToken != request.RefreshToken)
            {
                throw new BadRequestException("You need to login again");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

            var response = new RefreshResponseDTO { AccessToken = _tokenService.CreateJwtAccessToken(claims) };

            return response;
        }
    }
}
