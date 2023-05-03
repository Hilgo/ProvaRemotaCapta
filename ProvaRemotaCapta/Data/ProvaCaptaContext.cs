using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProvaRemotaCapta.Data.Models;

namespace ProvaRemotaCapta.Data;

public partial class ProvaCaptaContext : DbContext
{
    public ProvaCaptaContext()
    {
    }

    public ProvaCaptaContext(DbContextOptions<ProvaCaptaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Situacao> Situacoes { get; set; }

    public virtual DbSet<TipoCliente> TipoClientes { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //    => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("server=LAPTOP-J3ENK63D\\SQLEXPRESS;Database=ProvaCapta;Integrated Security=True;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Cpf, "UQ__Cliente__C1FF93096E3DEFFB").IsUnique();

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength();
            entity.Property(e => e.Nome).HasMaxLength(250);
            entity.Property(e => e.Sexo).HasMaxLength(200);

            entity.HasOne(d => d.Situacao).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdSituacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Situacao");

            entity.HasOne(d => d.TipoCliente).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Tipo");
        });

        modelBuilder.Entity<Situacao>(entity =>
        {
            entity.HasKey(e => e.IdSituacao);

            entity.ToTable("Situacao");

            entity.Property(e => e.NomeSituacao).HasMaxLength(250);
            entity.Property(e => e.SiglaSituacao)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<TipoCliente>(entity =>
        {
            entity.HasKey(e => e.IdTipoCliente);

            entity.ToTable("TipoCliente");

            entity.Property(e => e.Nome).HasMaxLength(250);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
