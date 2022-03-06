namespace KanjiDesu.Models
{
	public class Kanji
	{
		public string Character { get; set; }
		public Readings OnReadings { get; set; }
		public Readings KunReadings { get; set; }
		public List<string> Meanings { get; set; }
		public int JlptLevel { get; set; }

		public Kanji(string character, Readings onReadings, Readings kunReadings, int jlptLevel, List<string> meanings)
		{
			Character = character;
			JlptLevel = jlptLevel;
			OnReadings = onReadings;
			KunReadings = kunReadings;
			Meanings = meanings;
		}
	}
}