using Dooshka.Application.Features.DTOs.Auths;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Dooshka.Application.Features.AuthFeatures.Requests
{
    public class RefreshAccessTokenRequest : IRequest<RefreshResponseDTO>
    {
        [Required]
        public required string RefreshToken { get; set; }
    }
}
