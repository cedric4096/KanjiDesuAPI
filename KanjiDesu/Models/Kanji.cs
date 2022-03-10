using KanjiDesu.DataModels;

namespace KanjiDesu.Models
{
	/// <summary>
	/// Represents a kanji
	/// </summary>
	public class Kanji
	{
		/// <summary>
		/// Gets or sets the kanji for this isnstance
		/// </summary>
		public string Character { get; set; }
		/// <summary>
		/// Gets or sets the On <see cref="Readings"/> for this instance
		/// </summary>
		public Readings? OnReadings { get; set; }
		/// <summary>
		/// Gets or sets the Kun <see cref="Readings"/> for this instance
		/// </summary>
		public Readings? KunReadings { get; set; }
		/// <summary>
		/// Gets or sets the translations for this instance
		/// </summary>
		public List<string> Meanings { get; set; }
		/// <summary>
		/// Gets or sets the translations' language for this instance
		/// </summary>
		public string MeaningsLanguage { get; set; }
		/// <summary>
		/// Gets or sets the Japan Language Proficiency Test level for this instance
		/// </summary>
		public int JlptLevel { get; set; }

		public Kanji(KanjiDTO dto, Readings? onReadings, Readings? kunReadings)
		{
			Character = dto.Kanji;
			JlptLevel = (int)dto.Jlpt;

			if (!string.IsNullOrEmpty(dto.MeaningsFr))
			{
				MeaningsLanguage = "fr";
				Meanings = dto.MeaningsFr.Split(',').ToList();
			}
			else
			{
				MeaningsLanguage = "en";
				Meanings = dto.Meanings.Split(',').ToList();
			}

			OnReadings = onReadings;
			KunReadings = kunReadings;
		}
	}
}