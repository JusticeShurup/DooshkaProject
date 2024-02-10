using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dooshka.Application.Features.AuthFeatures.Handlers
{
    public class LogoutRequestHandler : IRequestHandler<LogoutRequest>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RevokedToken> _revokedTokenRepository;
        private readonly IHttpContextAccessor _httpContext;


        public LogoutRequestHandler(IRepository<User> userRepository, IRepository<RevokedToken> revokedTokenRepository, IHttpContextAccessor httpContext) 
        {
            _userRepository = userRepository;
            _revokedTokenRepository = revokedTokenRepository;
            _httpContext = httpContext;
        }

        public async Task Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContext.HttpContext.Items["User"]!;

            string accessToken = _httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
            
            try
            {
                await _revokedTokenRepository.CreateAsync(new RevokedToken() { Token = accessToken, RevokedAt = DateTime.UtcNow });
                await _revokedTokenRepository.CreateAsync(new RevokedToken() { Token = user.RefreshToken!, RevokedAt = DateTime.UtcNow});
            } catch (Exception ex)
            {

            }


            user.RefreshToken = "";

            await _userRepository.UpdateAsync(user);
        }
    }
}
