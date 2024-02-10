using Dooshka.Application.Features.DTOs;
using MediatR;

namespace Dooshka.Application.Features.UserFeatures.Requests
{
    public class ChangeNameRequest : IRequest<UserAccountDTO>
    {
        public required string NewName { get; set; }

    }
}
