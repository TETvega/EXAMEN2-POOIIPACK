using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen2Poo.API.Database.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        [Column("id")]
        public Guid Id { get; set; }

        // para la creacion
        [StringLength(50)]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        // para la modificacion
        [StringLength(50)]
        [Column("updated_by")]
        public string UpdateBy { get; set; }
        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        //public bool IsDeleted { get; set; }


    }
}
