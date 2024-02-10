using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dooshka.Domain
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;

        [Required]
        public required string Password { get; set; }

        public string? Name { get; set; }

        public string? RefreshToken { get; set; }
        
        [JsonIgnore]
        public List<ToDoItem>? ToDoItems { get; set; }


    }
}
