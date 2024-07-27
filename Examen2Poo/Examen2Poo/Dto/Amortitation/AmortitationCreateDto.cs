using Examen2Poo.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Dto.Amortitation
{
    public class AmortitationCreateDto
    {
        public Guid Id { get; set; }
        [Display(Name = "clave_amortizacion")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]

        public string NombreClave { get; set; }

        [Display(Name = "Fecha pago")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Dias")]
        public int DiasMes { get; set; }

        [Display(Name = "Numero de Cuota")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public int NCuota { get; set; }
        [Display(Name = "Tasa de Interes")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        public double Interest { get; set; }
        [Display(Name = "Abono")]
        public double Amount { get; set; }
        [Display(Name = "Seguro")]
        public double Svcd { get; set; }
        [Display(Name = "Seguro")]
        public double cuotaSinSvcd { get; set; }
        [Display(Name = "Abono Extraordinario")]
        public double AbonoExtraordinario { get; set; }
        [Display(Name = "Cuota Con seguro")]
        public double CuotaConSvcd { get; set; }
        [Display(Name = "Saldo Principal")]
        public double SaldoPrincipal { get; set; }

        public virtual IEnumerable<ClientAmortitationEntity> Clients { get; set; }
    }
}
