﻿using Dooshka.Application.Exceptions;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Middlewares
{
    public class TokenValidationMiddleware : IMiddleware
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RevokedToken> _revokedTokenRepository;


        public TokenValidationMiddleware([FromServices] IRepository<User> userRepository, [FromServices] IRepository<RevokedToken> revokedTokenRepository) 
        {
            _userRepository = userRepository;
            _revokedTokenRepository = revokedTokenRepository;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers["Authorization"].FirstOrDefault() == null)
            {
                await next.Invoke(context);
                return;
            }

            string tokenString = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

            JwtSecurityToken? token = new JwtSecurityToken(tokenString);

            var result =_revokedTokenRepository.Find(x => x.Token == tokenString);

            if (result != null)
            {
                throw new UnauthorizedException("Token revoked");
            }

            string? email = token.Payload.Claims.FirstOrDefault(x => x.Type.ToString() == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                throw new UnauthorizedException("Logical error");
            }

            User? user = _userRepository.Find(x => x.Email == email);

            if (user == null)
            {
                throw new UnauthorizedException("User didn't not found");
            }

            if (!user.IsEmailConfirmed)
            {
                throw new UnauthorizedException("Email isn't confirmed");
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email)
            });

            var principal = new ClaimsPrincipal(identity);

            context.User = principal;


            context.Items.Add("User", user);

            await next.Invoke(context);
        }

    }
}
