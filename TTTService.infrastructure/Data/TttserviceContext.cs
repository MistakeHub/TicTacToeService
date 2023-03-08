using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TTTService.Domain.Entities;

namespace TTTService.infrastructure.Data;

public partial class TttserviceContext : DbContext
{
    public TttserviceContext()
    {
    }

    public TttserviceContext(DbContextOptions<TttserviceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Move> Moves { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;username=root;password=.aA1234568_;database=tttservice;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("games");

            entity.HasIndex(e => e.Iduser1, "FK_FIRST_USER_ID_idx");

            entity.HasIndex(e => e.Iduser2, "FK_SECOND_USER_ID_idx");

            entity.Property(e => e.Dategamestart).HasColumnType("datetime");
          

            entity.HasOne(d => d.Iduser1Navigation).WithMany(p => p.GameIduser1Navigations)
                .HasForeignKey(d => d.Iduser1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FIRST_USER_ID");

            entity.HasOne(d => d.Iduser2Navigation).WithMany(p => p.GameIduser2Navigations)
                .HasForeignKey(d => d.Iduser2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SECOND_USER_ID");
        });

        modelBuilder.Entity<Move>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("moves");

            entity.HasIndex(e => e.Idgame, "FK_GAME_MOVE_idx");

            entity.HasIndex(e => e.Iduser, "FK_USER_MOVE_idx");

            entity.HasOne(d => d.IdgameNavigation).WithMany(p => p.Moves)
                .HasForeignKey(d => d.Idgame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GAME_MOVE");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Moves)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_MOVE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.Login).HasMaxLength(10);
            entity.Property(e => e.Password).HasMaxLength(70);
            entity.Property(e => e.Registerdate).HasColumnType("datetime");
           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
