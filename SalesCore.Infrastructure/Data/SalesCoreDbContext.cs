using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesCore.Domain.Entities;

namespace SalesCore.Infrastructure.Data;

public class SalesCoreDbContext : DbContext
{
    public SalesCoreDbContext(
        DbContextOptions<SalesCoreDbContext> options)
        : base(options)
    {
    }

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Contato> Contatos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa);
            entity.ToTable("EMPRESA");

            entity.Property(e => e.IdEmpresa)
                .HasColumnName("idEmpresa");

            entity.Property(e => e.RazaoSocial)
                  .HasColumnName("razaoSocial");

            entity.Property(e => e.NomeFantasia)
                  .HasColumnName("nomeFantasia");

            entity.Property(e => e.Cnpj)
                  .HasColumnName("cnpj");

            entity.Property(e => e.Email)
                  .HasColumnName("email");

            entity.Property(e => e.Telefone)
                  .HasColumnName("telefone");

            entity.Property(e => e.DataCadastro)
                  .HasColumnName("dataCadastro");

            entity.Property(e => e.Ativo)
                  .HasColumnName("ativo");

            
        });

        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(c => c.IdContato);

            entity.ToTable("Contato");

            entity.Property(c => c.IdContato)
                .HasColumnName("idContato");

            entity.Property(c => c.Nome)
                .HasColumnName("nome");

            entity.Property(c => c.Email)
                .HasColumnName("email");

            entity.Property(c => c.Telefone)
                .HasColumnName("telefone");

            entity.Property(c => c.Cargo)
                .HasColumnName("cargo");

            entity.Property(c => c.Ativo)
                .HasColumnName("ativo");

            entity.Property(c => c.IdEmpresa)
                .HasColumnName("idEmpresa");
            
            entity.HasOne(c => c.Empresa)
                .WithMany(e => e.Contatos)
                .HasForeignKey(c => c.IdEmpresa);
        });

    }
}


