using System;
namespace CassaAssistenzaSanitaria.API.Models
{
    public class RichiestaOutput
    {
        public int Id { get; set; }
        public int IdTipologia { get; set; }
        public int IdRichiedente { get; set; }
        public decimal ImportoFattura { get; set; }
        public decimal ImportoRimborsatoDaTerzi { get; set; }
        public decimal ImportoACarico { get; set; }
        public decimal ImportoDaRimborsare { get; set; }
        public string NumeroFattura { get; set; }
        public string Note { get; set; }
        public DateTime DataFattura { get; set; }
        public DateTime DataRichiesta { get; set; }
        public DateTime DataConferma { get; set; }
        public DateTime DataCancellazione { get; set; }
    }
}
