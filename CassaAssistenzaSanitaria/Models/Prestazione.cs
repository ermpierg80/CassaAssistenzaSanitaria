using System;

namespace CassaAssistenzaSanitaria.Models
{
    public class Prestazione
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public decimal PercentualeRimborso { get; set; }
        public DateTime DataCreazione { get; set; }
        public DateTime DataCancellazione { get; set; }
    }
}
