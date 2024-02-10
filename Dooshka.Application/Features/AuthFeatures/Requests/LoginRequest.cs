using Dooshka.Application.Features.DTOs;
using Dooshka.Application.Features.DTOs.Auths;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Features.AuthFeatures.Requests
{
    public class LoginRequest : IRequest<UserDTO>
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
