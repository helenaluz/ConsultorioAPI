using ConsultorioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){}

        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
             1 (medico) -> N (consultas)
             1 (paciente) -> N (consultas)
             */

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Consultas).WithOne(c => c.Paciente)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medico>()
                .HasMany(m => m.Consultas).WithOne(c => c.Medico)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Token>()
                .HasKey(t => new { t.Tabela, t.Id });
        }
    }
}
