using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Dto.Client
{
    public class ClientCreateDto
    {
        public Guid Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public string Name { get; set; }
        [Display(Name = "Identidad")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(13, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.")]
        public string IdentytyNumber { get; set; }
        [Display(Name = "Monto Desembolsado")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public double LoadAmount { get; set; }
        [Display(Name = "Tasa Comision")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public double ComisionRate { get; set; }
        [Display(Name = "Tasa de Interes")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public double InteresRest { get; set; }
        [Display(Name = "Termino")]
        [Required(ErrorMessage = "La {0} es obligatoria. en meses")]
        public int Ter { get; set; }
        [Display(Name = "Fecha de Desembolso")]
        [Required(ErrorMessage = "La {0} es obligatoria. en meses")]
        public DateTime DateTimeDesembolso { get; set; }
        [Display(Name = "Fecha del Primer Pago")]
        [Required(ErrorMessage = "La {0} es obligatoria. en meses")]
        public DateTime DeteTimePrimerPago { get; set; }
        public List<string> AmortitationsList { get; set; }
    }
}
