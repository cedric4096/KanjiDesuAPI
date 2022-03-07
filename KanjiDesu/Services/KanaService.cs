using KanjiDesu.DataModels;
using KanjiDesu.Helpers;
using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Service for <see cref="Kana"/> retrieval from DB
	/// </summary>
	public class KanaService : IKanaService
	{
		private readonly KanjiDesuContext context;

		public KanaService(KanjiDesuContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Retrieves all kana from DB
		/// </summary>
		/// <param name="difficulty"><see cref="Difficulty"/> of the searched kana</param>
		/// <param name="search">Romaji <see cref="string"/> to search in kana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns kana stricly in the specified difficulty, else returns kana in easier groups</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		public IEnumerable<Kana> Get(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return context.Kanas
				.Where(kana => (
						difficulty == null
						|| ((exclusive == null || !(bool)exclusive) && (Difficulty)kana.DifficultyGroup <= difficulty)
						|| (exclusive != null && (bool)exclusive && (Difficulty)kana.DifficultyGroup == difficulty)
					)
					&& (string.IsNullOrEmpty(search) || kana.Romaji.Contains(search))
				)
				.Select(kana => new Kana(
					kana.Kana, 
					kana.Romaji, 
					(Difficulty)kana.DifficultyGroup, 
					kana.IsHiragana
				)
			);
		}

		/// <summary>
		/// Retrieves all existing hiragana from DB
		/// </summary>
		/// <param name="difficulty"><see cref="Difficulty"/> of the searched hiragana</param>
		/// <param name="search">Romaji <see cref="string"/> to search in hiragana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns hiragana stricly in the specified difficulty, else returns hiragana in easier groups</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		public IEnumerable<Kana> GetHiragana(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return Get(difficulty, search, exclusive).Where(kana => kana.IsHiragana);
		}

		/// <summary>
		/// Retrieves all existing katakana from DB
		/// </summary>
		/// <param name="difficulty"><see cref="Difficulty"/> of the searched katakana</param>
		/// <param name="search">Romaji <see cref="string"/> to search in katakana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns latakana stricly in the specified difficulty, else returns katakana in easier groups</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		public IEnumerable<Kana> GetKatakana(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return Get(difficulty, search, exclusive).Where(kana => !kana.IsHiragana);
		}

		public Readings? BuildReadings(string? readings)
		{
			List<KeyValuePair<string, string>> transcriptions = new List<KeyValuePair<string, string>>();

			if (string.IsNullOrEmpty(readings)) return new Readings(transcriptions);

			List<string> KanaReadings = readings.Split(',').ToList();

			foreach (string reading in KanaReadings)
			{
				IEnumerable<Kana> kanas = GetInWord(reading);
				string res = "";
				int i = 0;

				while (i < reading.Length)
				{


					i++;
				}

				transcriptions.Add(new KeyValuePair<string, string>(reading, res));
			}

			return new Readings(transcriptions);
		}

		/// <summary>
		/// Retrieves all <see cref="Kana"/> in word
		/// </summary>
		/// <param name="word">The <see cref="string"/> containing the <see cref="Kana"/> to fetch</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing the required <see cref="Kana"/></returns>
		public IEnumerable<Kana> GetInWord(string word)
		{
			return context.Kanas
				.Where(kana => word.Contains(kana.Kana))
				.Select(kana => new Kana(
					kana.Kana,
					kana.Romaji,
					(Difficulty)kana.DifficultyGroup,
					kana.IsHiragana
				)
			);
		}
	}
}
