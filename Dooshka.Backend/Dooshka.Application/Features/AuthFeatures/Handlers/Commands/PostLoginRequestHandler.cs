using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs.Auths;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Application.Services;
using Dooshka.Domain;
using MediatR.Pipeline;
using System.Security.Claims;

namespace Dooshka.Application.Features.AuthFeatures.Handlers.Commands
{
    public class PostLoginRequestHandler : IRequestPostProcessor<LoginRequest, UserDTO>
    {
        private readonly TokenService _tokenService;
        private readonly IRepository<User> _repository;

        public PostLoginRequestHandler(TokenService tokenService, IRepository<User> repository)
        {
            _tokenService = tokenService;
            _repository = repository;
        }

        public async Task Process(LoginRequest request, UserDTO response, CancellationToken cancellationToken)
        {
            if (response == null)
            {
                return;
            }

            List<Claim> claims = new() { new Claim(ClaimTypes.Email, request.Email) };

            response.AccessToken = _tokenService.CreateJwtAccessToken(claims);
            response.RefreshToken = _tokenService.CreateJwtRefreshToken(claims);

            var user = _repository.Find(x => x.Email == request.Email);
            user!.RefreshToken = response.RefreshToken;

            await _repository.UpdateAsync(user);

        }
    }
}
