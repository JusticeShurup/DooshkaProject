using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Features.AuthFeatures.Requests
{
    public class ConfirmEmailRequest : IRequest
    {
        public required string Email { get; set; }
        public required int Code { get; set; }
    }
}
