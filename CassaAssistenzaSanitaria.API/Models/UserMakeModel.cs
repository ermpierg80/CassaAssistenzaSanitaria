using System;
using System.ComponentModel.DataAnnotations;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class UserMakeModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "EMail is required")]
        public string EMail { get; set; }
    }
}
