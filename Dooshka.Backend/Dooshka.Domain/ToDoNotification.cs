using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Domain
{
    [Table("ToDoNotifications")]
    public class ToDoNotification
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid ToDoItemId { get; set; }

        [ForeignKey("ToDoItemId")]
        public ToDoItem ToDoItem { get; set; }


        public string Message { get; set; } = ""; 
    }


}
