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
    }
}


