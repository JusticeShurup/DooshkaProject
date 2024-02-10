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
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, UserDTO> 
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher<User> _passwordHasher;


        public RegisterRequestHandler(IRepository<User> repository, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }


        public async Task<UserDTO> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            User user = new User { Email = request.Email, Password = request.Password, Name = request.Name };

            user.Password = _passwordHasher.HashPassword(user, request.Password);


            var existUser = _repository.Find(x => x.Id == user.Id);
            if (existUser != null)
            {
                throw new Exception("User already exist");
            }

            await _repository.CreateAsync(user);


            UserDTO userDTO = new() { Id = user.Id, Email = user.Email, Name = user.Name, AccessToken = "", RefreshToken = "" };


            return await Task.FromResult(userDTO);
        }
    }
}
