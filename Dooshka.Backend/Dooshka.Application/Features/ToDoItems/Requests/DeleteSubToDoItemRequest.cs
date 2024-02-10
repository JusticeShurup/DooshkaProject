using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class DeleteSubToDoItemRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
