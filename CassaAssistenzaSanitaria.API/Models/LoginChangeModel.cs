using System;
using System.ComponentModel.DataAnnotations;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class LoginChangeModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
    }
}
