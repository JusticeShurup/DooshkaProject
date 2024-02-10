using Dooshka.Domain;
using System.ComponentModel.DataAnnotations;

namespace Dooshka.Application.Features.DTOs.ToDoItems
{
    public class CreatedToDoItemDTO
    {
        [Required]
        public required Guid Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [EnumDataType(typeof(ToDoItemStatusType))]
        public ToDoItemStatusType Status { get; set; }

        public required DateOnly CreatedDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        public List<CreatedToDoItemDTO> SubItems { get; set; } = new List<CreatedToDoItemDTO> { };


    }
}
