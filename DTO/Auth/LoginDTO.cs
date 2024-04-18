using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Auth
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}