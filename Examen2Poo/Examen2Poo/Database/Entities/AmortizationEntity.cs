using Examen2Poo.API.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Database.Entities
{
    [Table("amortitation", Schema = "dbo")]
    public class AmortizationEntity:BaseEntity
    {
        [Display(Name = "identidad Cliente")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("identidad_cliente")]
        public string NombreClave {  get; set; }

        [Display(Name = "Numero de Cuota")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("NCuota")]
        public int NCuota { get; set; }
        [Display(Name = "Tasa de Interes")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("tasa_interes")]
        public double Interest { get; set; }
        [Display(Name = "Abono")]
        [Column("abono")]
        public double Amount { get; set; }
        [Display(Name = "Seguro")]
        [Column("seguro")]
        public double Svcd { get; set; }
        [Display(Name = "Seguro")]
        [Column("cuota_sin_Seguro")]
        public double cuotaSinSvcd { get; set; }
        [Display(Name = "Abono Extraordinario")]
        [Column("abono_extraordinario")]
        public double AbonoExtraordinario { get; set; }
        [Display(Name = "Cuota Con seguro")]
        [Column("cuota_con_seguro")]
        public double CuotaConSvcd { get; set; }
        [Display(Name = "Saldo Principal")]
        [Column("Saldo Principal")]
        public double SaldoPrincipal { get; set; }

        public virtual IEnumerable<ClientAmortitationEntity> Clients { get; set; }

    }
}
