using Dooshka.Application.Features.DTOs;
using Dooshka.Application.Features.UserFeatures.Requests;
using Dooshka.Application.Persistence.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dooshka.Application.Features.UserFeatures.Handlers.Commands
{
    public class ChangeNameRequestHandler : IRequestHandler<ChangeNameRequest, UserAccountDTO>
    {

        private readonly IRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangeNameRequestHandler(IHttpContextAccessor httpContextAccessor, IRepository<User> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;

        }

        public async Task<UserAccountDTO> Handle(ChangeNameRequest request, CancellationToken cancellationToken)
        {
            User user = (User)_httpContextAccessor.HttpContext.Items["User"];

            user.Name = request.NewName;

            await _userRepository.UpdateAsync(user);

            return new UserAccountDTO() { Email = user.Email, Name = user.Name };
        }
    }
}
