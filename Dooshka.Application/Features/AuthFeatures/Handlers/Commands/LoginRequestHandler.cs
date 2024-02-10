using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs;
using Dooshka.Application.Features.DTOs.Auths;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Features.AuthFeatures.Handlers.Commands
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, UserDTO>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginRequestHandler(IRepository<User> repository, IPasswordHasher<User> passwordHasher) 
        {
            _userRepository = repository;
            _passwordHasher = passwordHasher;
        }


        public async Task<UserDTO> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.Find(x => x.Email == request.Email);

            if (user == null)
            {
                throw new BadRequestException("User doesn't exists");
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Password invalid");
            }

            return new UserDTO { Id = user.Id, Email = user.Email, Name = user.Name, AccessToken = "", RefreshToken = "" };
        }
    }
}
