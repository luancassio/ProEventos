using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Moldels;

namespace ProEventos.Persistence.Data {
    public class ProEventosContext : IdentityDbContext<User, Role,int, 
                                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>> {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) {}

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur =>ur.Role )
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                 userRole.HasOne(ur =>ur.User )
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

            });

            /* colocando essa configuração ele consegue fazer a associação para junção das tabela
             eventos e palestrantes */
            modelBuilder.Entity<PalestranteEvento>().HasKey(
                PE => new { PE.EventoId, PE.PalestranteId });


            // configuração para ele deletar os dados de forma Cascade
            //deletar informação das outra tabelas que ele tem relacionamento
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedeSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedeSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
