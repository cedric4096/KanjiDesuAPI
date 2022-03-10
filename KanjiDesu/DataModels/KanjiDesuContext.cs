using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KanjiDesu.DataModels
{
    public partial class KanjiDesuContext : DbContext
    {
        public KanjiDesuContext()
        {
        }

        public KanjiDesuContext(DbContextOptions<KanjiDesuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kana> Kanas { get; set; } = null!;
        public virtual DbSet<Kanji> Kanjis { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:kanjidesuserver.database.windows.net,1433;Initial Catalog=kanjidesudb;Persist Security Info=False;User ID=kanjidesuadmin;Password=kanjidesustrongpassw0Rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kana>(entity =>
            {
                entity.ToTable("kana");

                entity.HasIndex(e => e.Utf, "UQ__kana__DD7774CFAD3BC51E")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DifficultyGroup).HasColumnName("difficulty_group");

                entity.Property(e => e.IsHiragana).HasColumnName("is_hiragana");

                entity.Property(e => e.Kana1)
                    .HasMaxLength(4)
                    .HasColumnName("kana");

                entity.Property(e => e.Romaji)
                    .HasMaxLength(10)
                    .HasColumnName("romaji");

                entity.Property(e => e.Utf)
                    .HasMaxLength(32)
                    .HasColumnName("utf");
            });

            modelBuilder.Entity<Kanji>(entity =>
            {
                entity.ToTable("kanji");

                entity.HasIndex(e => e.Utf, "UQ__kanji__DD7774CF3F69E2DA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.JaKun)
                    .HasMaxLength(4000)
                    .HasColumnName("ja_kun");

                entity.Property(e => e.JaOn)
                    .HasMaxLength(4000)
                    .HasColumnName("ja_on");

                entity.Property(e => e.Jlpt).HasColumnName("jlpt");

                entity.Property(e => e.Kanji1)
                    .HasMaxLength(2)
                    .HasColumnName("kanji");

                entity.Property(e => e.Meanings)
                    .HasMaxLength(4000)
                    .HasColumnName("meanings");

                entity.Property(e => e.MeaningsFr)
                    .HasMaxLength(4000)
                    .HasColumnName("meanings_fr");

                entity.Property(e => e.Utf)
                    .HasMaxLength(32)
                    .HasColumnName("utf");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("player");

                entity.HasIndex(e => e.Pseudo, "UQ__player__EA0EEA226C9080C9")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BestScore).HasColumnName("best_score");

                entity.Property(e => e.Pseudo)
                    .HasMaxLength(60)
                    .HasColumnName("pseudo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
