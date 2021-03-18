using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class Iscritto
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Cognome { get; set; }
        [Required]
        [Column(TypeName = "varchar(16)")]
        public string CodiceFiscale { get; set; }
        [Required]
        [Column(TypeName = "varchar(27)")]
        public string IBAN { get; set; }

        public DateTime DataIscrizione { get; set; }
		public DateTime DataCancellazione { get; set; }

        public string GetNomeCompleto()
        {
            return this.Nome + " " + this.Cognome;
        }
        public override string ToString()
        {
            return "Iscritto [id=" + this.Id.ToString() + ", Nome=" + this.GetNomeCompleto() + ", CodiceFiscale=" + this.CodiceFiscale + "]";
        }
    }
}
