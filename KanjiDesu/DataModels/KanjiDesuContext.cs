using Microsoft.EntityFrameworkCore;

namespace KanjiDesu.DataModels
{
	public partial class KanjiDesuContext : DbContext
	{
		public KanjiDesuContext(DbContextOptions<KanjiDesuContext> options)
			: base(options)
		{}

		public virtual DbSet<KanaDTO> Kanas { get; set; } = null!;
		public virtual DbSet<KanjiDTO> Kanjis { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=tcp:kanjidesuserver.database.windows.net,1433;Initial Catalog=kanjidesudb;Persist Security Info=False;User ID=kanjidesuadmin;Password=kanjidesustrongpassw0Rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<KanaDTO>(entity =>
			{
				entity.ToTable("kana");

				entity.HasIndex(e => e.Utf, "UQ__kana__DD7774CFD0E45F2D")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.DifficultyGroup).HasColumnName("difficulty_group");

				entity.Property(e => e.IsHiragana).HasColumnName("is_hiragana");

				entity.Property(e => e.Kana)
					.HasMaxLength(4)
					.HasColumnName("kana");

				entity.Property(e => e.Romaji)
					.HasMaxLength(10)
					.HasColumnName("romaji");

				entity.Property(e => e.Utf)
					.HasMaxLength(32)
					.HasColumnName("utf");
			});

			modelBuilder.Entity<KanjiDTO>(entity =>
			{
				entity.ToTable("kanji");

				entity.HasIndex(e => e.Utf, "UQ__kanji__DD7774CF40D4C500")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.KunReadings)
					.HasMaxLength(4000)
					.HasColumnName("ja_kun");

				entity.Property(e => e.OnReadings)
					.HasMaxLength(4000)
					.HasColumnName("ja_on");

				entity.Property(e => e.Jlpt).HasColumnName("jlpt");

				entity.Property(e => e.Kanji)
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

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
