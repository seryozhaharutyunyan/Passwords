using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Passwords.Model
{
    public class Data : Model
    {

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [Column(TypeName = "TEXT")]
        public string SiteName { get; set; } = null!;

        [Required]
        [Column(TypeName = "TEXT")]
        public string EmailOrPone { get; set; } = null!;

        [Required]
        [JsonIgnore]
        [MinLength(8)]
        //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\\S")]
        [Column(TypeName = "TEXT")]
        public string Password { get; set; } = null!;

    }
}
