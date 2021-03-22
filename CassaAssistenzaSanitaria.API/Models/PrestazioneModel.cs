using System.ComponentModel.DataAnnotations;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class PrestazioneModel
    {
        [Required(ErrorMessage = "Descrizione is required")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "PercentualeRimborso is required")]
        public string PercentualeRimborso { get; set; }

        [Required(ErrorMessage = "DataCrezione is required")]
        public string DataCrezione { get; set; }

        [Required(ErrorMessage = "DataCancellazione is required")]
        public string DataCancellazione { get; set; }
    }
}
