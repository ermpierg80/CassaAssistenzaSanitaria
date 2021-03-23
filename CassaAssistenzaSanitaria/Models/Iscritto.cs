using System;

namespace CassaAssistenzaSanitaria.Models
{
    public class Iscritto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }
        public string IBAN { get; set; }
        public DateTime DataIscrizione { get; set; }
        public DateTime DataCancellazione { get; set; }
    }
}
