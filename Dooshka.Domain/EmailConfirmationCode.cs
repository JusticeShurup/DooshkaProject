using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Domain
{

    [Table("EmailConfirmationCodes")]
    public class EmailConfirmationCode
    {
        [Key]
        public required string Email { get; set; }

        [Required]
        public required int Code { get; set; }
    }
}
