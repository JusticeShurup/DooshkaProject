using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Features.DTOs;
using Dooshka.Application.Features.DTOs.Auths;
using Dooshka.Application.Infrastructure.Contracts;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Application.Services;
using Dooshka.Domain;
using MediatR.Pipeline;
using System.Security.Claims;

namespace Dooshka.Application.Features.AuthFeatures.Handlers.Commands
{
    public class PostRegisterRequestHandler : IRequestPostProcessor<RegisterRequest, UserDTO>
    {
        
        private readonly TokenService _tokenService;
        private readonly IRepository<User> _repository;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<EmailConfirmationCode> _codesRepository;


        public PostRegisterRequestHandler(TokenService tokenService, IRepository<User> repository, IEmailSender emailSender, IRepository<EmailConfirmationCode> codesRepository)
        {
            _tokenService = tokenService;
            _repository = repository;
            _emailSender = emailSender;
            _codesRepository = codesRepository;
        }

        public async Task Process(RegisterRequest request, UserDTO response, CancellationToken cancellationToken)
        {
            if (response == null)
            {
                return;
            }

            List<Claim> claims = new() { new Claim(ClaimTypes.Email, request.Email) };

            response.AccessToken = _tokenService.CreateJwtAccessToken(claims);
            response.RefreshToken = _tokenService.CreateJwtRefreshToken(claims);

            var user = _repository.Find(x => x.Email == request.Email);
            user!.RefreshToken = response.RefreshToken;

            int confirmationCode = new Random().Next(1000, 9999);

            await _codesRepository.CreateAsync(new EmailConfirmationCode() { Code = confirmationCode, Email = user.Email });

            _emailSender.SendConfirmationCode(user.Email, confirmationCode);
            
            await _repository.UpdateAsync(user);

        }
    }
}
