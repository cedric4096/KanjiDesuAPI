using KanjiDesu.Helpers;

namespace KanjiDesu.Models
{
	public class Kana
	{
		public string Character { get; set; }
		public string Romaji { get; set; }
		public Difficulty DifficultyGroup { get; set; }
		public bool IsHiragana { get; set; }

		public Kana(string character, string romaji, Difficulty difficultyGroup, bool isHiragana)
		{
			Character = character;
			Romaji = romaji;
			DifficultyGroup = difficultyGroup;
			IsHiragana = isHiragana;
		}
	}
}
