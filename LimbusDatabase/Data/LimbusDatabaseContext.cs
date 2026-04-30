using System;
using System.Collections.Generic;
using LimbusDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Data;

public partial class LimbusDatabaseContext : DbContext
{
    public LimbusDatabaseContext()
    {
    }

    public LimbusDatabaseContext(DbContextOptions<LimbusDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anormalite> Anormalites { get; set; }

    public virtual DbSet<Bataille> Batailles { get; set; }

    public virtual DbSet<Changelog> Changelogs { get; set; }

    public virtual DbSet<Equipement> Equipements { get; set; }

    public virtual DbSet<Faction> Factions { get; set; }

    public virtual DbSet<Identite> Identites { get; set; }

    public virtual DbSet<Personnage> Personnages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=LimbusDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anormalite>(entity =>
        {
            entity.HasKey(e => e.AnormaliteId).HasName("PK_Anormalite_AnormaliteID");
        });

        modelBuilder.Entity<Bataille>(entity =>
        {
            entity.HasKey(e => e.BatailleId).HasName("PK_Bataille_BatailleID");

            entity.HasMany(d => d.Anormalites).WithMany(p => p.Batailles)
                .UsingEntity<Dictionary<string, object>>(
                    "AnormaliteBataille",
                    r => r.HasOne<Anormalite>().WithMany()
                        .HasForeignKey("AnormaliteId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AnormaliteBataille_AnormaliteID"),
                    l => l.HasOne<Bataille>().WithMany()
                        .HasForeignKey("BatailleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_AnormaliteBataille_BatailleID"),
                    j =>
                    {
                        j.HasKey("BatailleId", "AnormaliteId").HasName("PK_AnormaliteBataille_BatailleID_AnormaliteID");
                        j.ToTable("AnormaliteBataille", "Anormalites");
                        j.IndexerProperty<int>("BatailleId").HasColumnName("BatailleID");
                        j.IndexerProperty<int>("AnormaliteId").HasColumnName("AnormaliteID");
                    });

            entity.HasMany(d => d.Personnages).WithMany(p => p.Batailles)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonnageBataille",
                    r => r.HasOne<Personnage>().WithMany()
                        .HasForeignKey("PersonnageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PersonnageBataille_PersonnageID"),
                    l => l.HasOne<Bataille>().WithMany()
                        .HasForeignKey("BatailleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PersonnageBataille_BatailleID"),
                    j =>
                    {
                        j.HasKey("BatailleId", "PersonnageId").HasName("PK_PersonnageBataille_BatailleID_PersonnageID");
                        j.ToTable("PersonnageBataille", "Anormalites");
                        j.IndexerProperty<int>("BatailleId").HasColumnName("BatailleID");
                        j.IndexerProperty<int>("PersonnageId").HasColumnName("PersonnageID");
                    });
        });

        modelBuilder.Entity<Changelog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__changelo__3213E83F8B0C0A9C");

            entity.Property(e => e.InstalledOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Equipement>(entity =>
        {
            entity.HasKey(e => e.EquipementId).HasName("PK_Equipement_EquipementID");

            entity.HasOne(d => d.Anormalite).WithMany(p => p.Equipements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Equipement_AnormaliteID");

            entity.HasOne(d => d.Personnage).WithMany(p => p.Equipements).HasConstraintName("FK_Equipement_PersonnageID");
        });

        modelBuilder.Entity<Faction>(entity =>
        {
            entity.HasKey(e => e.FactionId).HasName("PK_Faction_FactionID");
        });

        modelBuilder.Entity<Identite>(entity =>
        {
            entity.Property(e => e.IdentiteId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Personnage).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Identite_PersonnageID");
        });

        modelBuilder.Entity<Personnage>(entity =>
        {
            entity.HasKey(e => e.PersonnageId).HasName("PK_Personnage_PersonnageID");

            entity.Property(e => e.District).IsFixedLength();
            entity.Property(e => e.Nom).HasDefaultValue("");

            entity.HasOne(d => d.Faction).WithMany(p => p.Personnages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personnage_FactionID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
