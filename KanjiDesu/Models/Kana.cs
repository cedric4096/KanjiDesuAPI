using KanjiDesu.Helpers;

namespace KanjiDesu.Models
{
	public class Kana
	{
		/// <summary>
		/// Gets or sets the kana for this instance
		/// </summary>
		public string Character { get; set; }
		/// <summary>
		/// Gets or sets the rōmaji transcription for this instance
		/// </summary>
		public string Romaji { get; set; }
		/// <summary>
		/// Gets or sets the difficulty group for this instance
		/// </summary>
		public Difficulty DifficultyGroup { get; set; }
		/// <summary>
		/// Gets or sets if the current instance is representing a hiragana (<see langword="true"/>) or a katakana (<see langword="false"/>)
		/// </summary>
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
