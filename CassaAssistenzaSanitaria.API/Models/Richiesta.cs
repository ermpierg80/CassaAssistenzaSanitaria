using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class Richiesta
    {
        public int Id { get; set; }
        public virtual Prestazione Tipologia { get; set; }
	    public virtual Iscritto Richiedente { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal ImportoFattura { get; set;}
        [Column(TypeName = "decimal(7,2)")]
        public decimal ImportoRimborsatoDaTerzi { get; set;}
        [Column(TypeName = "decimal(7,2)")]
        public decimal ImportoACarico { get; set;}
        [Column(TypeName = "decimal(7,2)")]
        public decimal ImportoDaRimborsare { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string NumeroFattura { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string Note { get; set; }
        public DateTime DataFattura { get; set; }
	    public DateTime DataRichiesta { get; set; }
        public DateTime DataConferma { get; set; }
        public DateTime DataCancellazione { get; set; }

        public override string ToString()
        {
            return "Richiesta [id=" + this.Id.ToString() + ", Prestazione=" + this.Tipologia.ToString() + ", Richiedente=" + this.Richiedente.ToString() + ", ImportoFattura=" + this.ImportoFattura.ToString() + ", DataFattura=" + this.DataFattura.ToString() + "]";
        }
    }
}
