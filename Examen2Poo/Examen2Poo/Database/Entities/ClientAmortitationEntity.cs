using Examen2Poo.API.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.Database.Entities
{
    [Table("client_amortitation", Schema = "dbo")]
    public class ClientAmortitationEntity:BaseEntity
    {
        [Column("client_id")]
        public virtual Guid ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]

        public ClientEntity Clients { get; set; }
        [Column("amortitation_id")]
        public virtual Guid AmortizationId { get; set; }

        [ForeignKey(nameof(AmortizationId))]
        public AmortizationEntity Amortization { get; set; }

    }
}
