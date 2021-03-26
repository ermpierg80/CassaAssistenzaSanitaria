using System;
namespace CassaAssistenzaSanitaria.Models
{
    public class RichiestaSend
    {
        public string Id { get; set; }
        public string IdTipologia { get; set; }
        public string IdRichiedente { get; set; }
        public string ImportoFattura { get; set; }
        public string ImportoRimborsatoDaTerzi { get; set; }
        public string ImportoACarico { get; set; }
        public string ImportoDaRimborsare { get; set; }
        public string NumeroFattura { get; set; }
        public string Note { get; set; }
        public DateTime DataFattura { get; set; }
        public DateTime DataRichiesta { get; set; }
        public DateTime DataConferma { get; set; }
        public DateTime DataCancellazione { get; set; }
    }
}
