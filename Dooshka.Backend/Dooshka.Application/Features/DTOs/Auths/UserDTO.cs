using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Features.DTOs.Auths
{
    public class UserDTO
    {
        public required Guid Id { get; set; }

        public required string Email { get; set; }

        public string? Name { get; set; }

        public string AccessToken { get; set; } = "";

        public string RefreshToken { get; set; } = "";
    }
}
