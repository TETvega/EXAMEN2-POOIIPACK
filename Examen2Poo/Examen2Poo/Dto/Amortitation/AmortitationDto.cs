using Examen2Poo.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examen2Poo.Dto.Amortitation
{
    public class AmortitationDto
    {
        public string NombreClave { get; set; }
        public DateTime Fecha { get; set; }
        public int DiasMes { get; set; }
        public int NCuota { get; set; }
        public double Interest { get; set; }
        public double Amount { get; set; }
        public double Svcd { get; set; }
        public double cuotaSinSvcd { get; set; }
        public double AbonoExtraordinario { get; set; }
        public double CuotaConSvcd { get; set; }
        public double SaldoPrincipal { get; set; }

        public virtual IEnumerable<ClientAmortitationEntity> Clients { get; set; }
    }
}
