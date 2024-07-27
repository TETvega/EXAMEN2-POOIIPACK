using Examen2Poo.API.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Database.Entities
{

    [Table("client", Schema = "dbo")]
    public class ClientEntity : BaseEntity
    {

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("nombre")]
        public string Name { get; set; }
        [Display(Name = "Identidad")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(13, ErrorMessage = "La {0} debe tener al menos {2} caracteres de longitud.")]
        [Column("identidad")]
        public int IdentytyNumber { get; set; }
        [Display(Name = "Monto Desembolsado")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("monto_desembolsado")]
        public double LoadAmount    { get; set; }
        [Display(Name = "Tasa Comision")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("tasa_comision")]
        public double ComisionRate { get; set; }
        [Display(Name = "Tasa de Interes")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [Column("tasa_interes")]
        public double InteresRest { get; set; }
        [Display(Name = "Termino")]
        [Required(ErrorMessage = "La {0} es obligatoria. en meses")]
        [Column("termino_pagos")]
        public int Ter {  get; set; }
        [Display(Name = "Fecha de Desembolso")]
        [Required(ErrorMessage = "La {0} es obligatoria. en meses")]
        [Column("fecha_desembolso")]
        public DateTime DateTimeDesembolso { get; set; }
        [Display(Name = "Fecha del Primer Pago")]
        [Required(ErrorMessage = "La {0} es obligatoria. en meses")]
        [Column("Fecha del Primer Pago")]
        public DateTime DeteTimePrimerPago { get; set; }

        public virtual IEnumerable<ClientAmortitationEntity> Amortitations { get; set; }

    }
}
