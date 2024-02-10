namespace BLL.ToDoItemsLogic.DTOs
{
    public class UpdatedToDoItemDTO 
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateOnly CreatedDate { get; set; }
        public DateOnly CompletionDate { get; set; }

        public List<UpdatedToDoItemDTO> SubItems { get; set; } = new List<UpdatedToDoItemDTO>();

    }
}
