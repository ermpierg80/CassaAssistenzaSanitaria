using System.ComponentModel.DataAnnotations;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class IscrittoModel
    {
        [Required(ErrorMessage = "Nome is required")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Cognome is required")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "CodiceFiscale is required")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "IBAN is required")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "DataIscrizione is required")]
        public string DataIscrizione { get; set; }

        [Required(ErrorMessage = "DataCancellazione is required")]
        public string DataCancellazione { get; set; }
    }
}
