using Examen2Poo.API.Database.Entities;
using Examen2Poo.API.Services.Interfaces;
using Examen2Poo.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Examen2Poo.Database
{
    public class Examen2PooContext: DbContext
    {
        private readonly IAuthService _authService;

        // las ociones son los parametros de configuracion a la base de datos
        public Examen2PooContext(
            DbContextOptions options,
            IAuthService authService
            ) : base(options)
        {
            _authService = authService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Aca iria nuestro codigo
            var entries = ChangeTracker.Entries()
                .Where(j => j.Entity is BaseEntity && (
                    j.State == EntityState.Added || j.State == EntityState.Modified
                ));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    //cuendo agrega
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _authService.GetUserId();
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        // cuenado edita
                        entity.UpdateBy = _authService.GetUserId();
                        entity.UpdatedDate = DateTime.Now;
                    }

                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        public  DbSet<ClientEntity> Clients { get; set; }
        public DbSet<AmortizationEntity> Amortitation { get; set; }
        public DbSet<ClientAmortitationEntity> ClientAmortitation { get; set; }

    }
}
