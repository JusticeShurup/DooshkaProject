using Dooshka.Application.Features.DTOs.ToDoItems;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Dooshka.Application.Features.ToDoItems.Requests
{
    public class CreateSubToDoItemRequest : IRequest<CreatedToDoItemDTO>
    {
        [Required]
        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public required string CompletionDate { get; set; }

        public required Guid ParentId { get; set; }
    }

}
