using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class UpdateToDoItemRequest : IRequest<UpdatedToDoItemDTO>
    {
        public required Guid Id { get; set; }
        
        public required string Title { get; set; }
        
        public required string Description { get; set; } = string.Empty;

        public required string CompletionDate { get; set; }
    }
}
