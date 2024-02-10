using System.ComponentModel.DataAnnotations;

namespace Dooshka.Application.Features.DTOs.Auths
{
    public class RefreshResponseDTO
    {
        [Required]
        public required string AccessToken { get; set; }
    }
}
