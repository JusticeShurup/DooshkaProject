using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class DeleteToDoItemRequest : IRequest<DeleteToDoItemResponseDTO>
    {
        public Guid Id;

    }
}
