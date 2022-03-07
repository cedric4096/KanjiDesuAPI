using KanjiDesu.DataModels;

namespace KanjiDesu.Models
{
	public class Kanji
	{
		public string Character { get; set; }
		public Readings? OnReadings { get; set; }
		public Readings? KunReadings { get; set; }
		public List<string> Meanings { get; set; }
		public string MeaningsLanguage { get; set; }
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