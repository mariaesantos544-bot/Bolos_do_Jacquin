using System;
using System.Collections.Generic;
using Bolos_do_Jacquin.Models;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.BdContextEvent;

public partial class BolosDoJacquinContext : DbContext
{
    public BolosDoJacquinContext()
    {
    }

    public BolosDoJacquinContext(DbContextOptions<BolosDoJacquinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avaliacao> Avaliacao { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Produto> Produto { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D09S20-1252894\\SQLEXPRESS2;Database=Bolos_do_Jacquin;User Id=sa;Password=Senai@134;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.IdAvaliacao).HasName("PK__Avaliaca__78C432D8631F8720");

            entity.Property(e => e.IdAvaliacao).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdProdutoNavigation).WithMany(p => p.Avaliacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Avaliacao__IdPro__6B24EA82");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Avaliacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Avaliacao__IdUsu__6A30C649");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A106EBB92B5");

            entity.Property(e => e.IdCategoria).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.IdProduto).HasName("PK__Produto__2E883C23DA2809E6");

            entity.Property(e => e.IdProduto).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Produto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Produto__IdCateg__656C112C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9782A35712");

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
