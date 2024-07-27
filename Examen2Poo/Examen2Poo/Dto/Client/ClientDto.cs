using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Examen2Poo.Database.Entities;

namespace Examen2Poo.Dto.Client
{
    public class ClientDto
    {


        public string Name { get; set; }

        public string IdentytyNumber { get; set; }

        public double LoadAmount { get; set; }

        public double ComisionRate { get; set; }

        public double InteresRest { get; set; }

        public int Ter { get; set; }

        public DateTime DateTimeDesembolso { get; set; }
        public DateTime DeteTimePrimerPago { get; set; }
        public virtual IEnumerable<ClientAmortitationEntity> Amortitations { get; set; }
    }
}
