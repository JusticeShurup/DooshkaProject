using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class GetToDoItemsByDateRequest : IRequest<List<CreatedToDoItemDTO>>
    {
        public DateOnly Date { get; set; }
    }
}
