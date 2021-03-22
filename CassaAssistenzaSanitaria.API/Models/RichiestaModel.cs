using System.ComponentModel.DataAnnotations;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class RichiestaModel
    { 
        [Required(ErrorMessage = "IdTipologia is required")]
        public string IdTipologia { get; set; }

        [Required(ErrorMessage = "IdRichiedente is required")]
        public string IdRichiedente { get; set; }

        [Required(ErrorMessage = "ImportoFattura is required")]
        public string ImportoFattura { get; set; }

        [Required(ErrorMessage = "ImportoRimborsatoDaTerzi is required")]
        public string ImportoRimborsatoDaTerzi { get; set; }

        [Required(ErrorMessage = "ImportoACarico is required")]
        public string ImportoACarico { get; set; }

        [Required(ErrorMessage = "ImportoDaRimborsare is required")]
        public string ImportoDaRimborsare { get; set; }

        [Required(ErrorMessage = "NumeroFattura is required")]
        public string NumeroFattura { get; set; }

        [Required(ErrorMessage = "Note is required")]
        public string Note { get; set; }

        [Required(ErrorMessage = "DataFattura is required")]
        public string DataFattura { get; set; }

        [Required(ErrorMessage = "DataRichiesta is required")]
        public string DataRichiesta { get; set; }

        [Required(ErrorMessage = "DataConferma is required")]
        public string DataConferma { get; set; }

        [Required(ErrorMessage = "DataCancellazione is required")]
        public string DataCancellazione { get; set; }
    }
}
