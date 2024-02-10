using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.UserFeatures.Requests;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Dooshka.Application.Features.UserFeatures.Handlers.Commands
{
    public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordHasher<User> _passwordHasher;


        public ChangePasswordRequestHandler(IRepository<User> userRepository, IHttpContextAccessor contextAccessor, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _httpContextAccessor = contextAccessor;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContextAccessor.HttpContext.Items["User"];

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Password incorrect");
            }

            user.Password = _passwordHasher.HashPassword(user, request.NewPassword);

            await _userRepository.UpdateAsync(user);
        }
    }
}
