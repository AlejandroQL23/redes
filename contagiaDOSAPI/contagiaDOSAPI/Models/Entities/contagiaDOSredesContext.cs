using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace contagiaDOSAPI.Models.Entities
{
    public partial class contagiaDOSredesContext : DbContext
    {
        public contagiaDOSredesContext()
        {
        }

        public contagiaDOSredesContext(DbContextOptions<contagiaDOSredesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Round> Round { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=163.178.107.10;Initial Catalog=contagiaDOSredes;User ID=laboratorios;Password=KmZpo.2796");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20);

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(20);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20);

                entity.Property(e => e.PlayerId).HasColumnName("player_id");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("FK_Group_Player");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GameId).HasColumnName("gameId");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20);

                entity.Property(e => e.Psycho).HasColumnName("psycho");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_Player_Game");
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Leader)
                    .HasColumnName("leader")
                    .HasMaxLength(20);

                entity.Property(e => e.Psychowin).HasColumnName("psychowin");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Round)
                    .HasForeignKey<Round>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Round_Game");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
