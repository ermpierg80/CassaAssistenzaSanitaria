using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class Prestazione
    {
		public int Id { get; set; }
		[Required]
		[Column(TypeName = "varchar(255)")]
		public string Descrizione { get; set; }
		[Column(TypeName = "decimal(5,2)")]
		public decimal PercentualeRimborso { get; set; }
		public DateTime DataCreazione { get; set; }
		public DateTime DataCancellazione { get; set; }
		
		public override string ToString()
        {
            return "Iscritto [id=" + this.Id + ", Descrizione=" + this.Descrizione + "]";
        }
    }
}
