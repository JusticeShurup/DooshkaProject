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
    /// <summary>
    /// Request to register User
    /// </summary>
    public class RegisterRequest : IRequest<UserDTO>
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        public string? Name { get; set; }
    }
}
