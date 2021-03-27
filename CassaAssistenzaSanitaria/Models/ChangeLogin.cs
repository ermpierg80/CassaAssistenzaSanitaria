using System;
namespace CassaAssistenzaSanitaria.Models
{
    public class ChangeLogin
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
