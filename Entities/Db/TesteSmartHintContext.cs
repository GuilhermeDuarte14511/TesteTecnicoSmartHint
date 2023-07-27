using Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using TesteSmartHint.Models;

namespace Entities.Db
{
    public class TesteSmartHintContext : DbContext, ITesteSmartHintContext
    {
        public DbSet<Clientes> Clientes { get; set; }
        public TesteSmartHintContext(DbContextOptions<TesteSmartHintContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.ToTable("clientes");
                entity.HasKey(e => e.Id).HasName("PK_clientes");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Nome).HasColumnName("Nome");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Telefone).HasColumnName("Telefone");
                entity.Property(e => e.DataCadastro).HasColumnName("DataCadastro");
                entity.Property(e => e.Bloqueado).HasColumnName("Bloqueado");
                entity.Property(e => e.TipoPessoa).HasColumnName("TipoPessoa");
                entity.Property(e => e.Documento).HasColumnName("Documento");
                entity.Property(e => e.InscricaoEstadual).HasColumnName("InscricaoEstadual");
                entity.Property(e => e.Isento).HasColumnName("Isento");
                entity.Property(e => e.Genero).HasColumnName("Genero");
                entity.Property(e => e.DataNascimento).HasColumnName("DataNascimento");
                entity.Property(e => e.Senha).HasColumnName("Senha");
            });

        }
    }
}
