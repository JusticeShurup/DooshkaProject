using System.ComponentModel.DataAnnotations;

namespace Dooshka.Application.Features.DTOs.ToDoItems
{
    public class ToDoItemForCreateDTO
    {
        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public string? CompletionDate { get; set; }

        public Guid? ParentId { get; set; }

    }
}
