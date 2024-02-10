using MediatR;

namespace Dooshka.Application.Features.UserFeatures.Requests
{
    public class ChangePasswordRequest : IRequest
    {
        public required string Password { get; set; }
        public required string NewPassword { get; set; }
    }
}
