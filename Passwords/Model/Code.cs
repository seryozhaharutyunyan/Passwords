using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passwords.Model
{
    public class Code : Model
    {
       
        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        [Column(TypeName="TEXT")]
        public string Key { get; set; } = null!;
    }
}
