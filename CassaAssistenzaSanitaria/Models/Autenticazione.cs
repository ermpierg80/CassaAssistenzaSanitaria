using System;
namespace CassaAssistenzaSanitaria.Models
{
    public class Autenticazione
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}
