using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dooshka.Domain
{
    [Table("RevokedTokens")]
    public class RevokedToken
    {
        [Key]
        public required string Token { get; set; }

        [Required]
        public required DateTime RevokedAt { get; set; }
    }
}
