using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CassaAssistenzaSanitaria.Models
{
    public class HealtCareItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Tipologia { get; set; }
        public int Richiedente { get; set; }
        public decimal ImportoFattura { get; set; }
        public decimal ImportoRimborsatoDaTerzi { get; set; }
        public decimal ImportoACarico { get; set; }
        public decimal ImportoDaRimborsare { get; set; }
        public string NumeroFattura { get; set; }
        public string Note { get; set; }
        public DateTime DataFattura { get; set; }
        public DateTime DataRichiesta { get; set; }
        public DateTime DataConferma { get; set; }
        public DateTime DataTrasmissione { get; set; }
        public bool Conferma { get; set; }
        public bool Trasmessa { get; set; }
    }
}
