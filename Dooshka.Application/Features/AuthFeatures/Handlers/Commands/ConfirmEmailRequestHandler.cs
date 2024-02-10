using Dooshka.Application.Exceptions;
using Dooshka.Application.Features.AuthFeatures.Requests;
using Dooshka.Application.Persistance.Contracts;
using Dooshka.Domain;
using MediatR;

namespace Dooshka.Application.Features.AuthFeatures.Handlers
{
    public class ConfirmEmailRequestHandler : IRequestHandler<ConfirmEmailRequest>
    {
        private readonly IRepository<EmailConfirmationCode> _codesRepository;
        private readonly IRepository<User> _userRepository;

        public ConfirmEmailRequestHandler(IRepository<EmailConfirmationCode> codesRepository, IRepository<User> userRepository)
        {
            _codesRepository = codesRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var confirmationCode = _codesRepository.Find(x => x.Email == request.Email);
            var user = _userRepository.Find(x => x.Email == request.Email);

            if (confirmationCode == null || user == null)
            {
                throw new BadRequestException("");
            }

            if (confirmationCode.Code != request.Code)
            {
                throw new BadRequestException("");
            }

            user.IsEmailConfirmed = true;
            
            await _userRepository.UpdateAsync(user);
            await _codesRepository.DeleteAsync(confirmationCode);
        }
    }
}
