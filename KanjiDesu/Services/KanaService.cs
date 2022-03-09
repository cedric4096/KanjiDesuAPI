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
		/// <param name="search">Rōmaji <see cref="string"/> to search in kana</param>
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
		/// <param name="search">Rōmaji <see cref="string"/> to search in hiragana</param>
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
		/// <param name="search">Rōmaji <see cref="string"/> to search in katakana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns latakana stricly in the specified difficulty, else returns katakana in easier groups</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		public IEnumerable<Kana> GetKatakana(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return Get(difficulty, search, exclusive).Where(kana => !kana.IsHiragana);
		}

		/// <summary>
		/// Builds the rōmaji transcription of a kana reading
		/// </summary>
		/// <param name="readings">The reading to transcribe</param>
		/// <returns>The rōmaji reading</returns>
		public Readings? BuildReadings(string? readings)
		{
			Dictionary<string, string> transcriptions = new Dictionary<string, string>();

			if (string.IsNullOrEmpty(readings)) return new Readings(transcriptions);

			HashSet<string> KanaReadings = readings.Split(',').ToHashSet();

			foreach (string reading in KanaReadings)
			{
				List<Kana> kanas = GetInWord(reading).ToList();
				string res = "";
				bool isPreviousLtsu = false;
				int i = 0;

				while (i < reading.Length)
				{
					IEnumerable<Kana> foundForChar = kanas.Where(k => k.Character.Contains(reading[i]));
					if (foundForChar.Any())
					{
						if (foundForChar.Count() == 1)
						{
							Kana found = foundForChar.First();

							if (found.DifficultyGroup == Difficulty.LittleCharsBefore)
							{
								isPreviousLtsu = true;
							}
							else if (found.DifficultyGroup == Difficulty.VowelProlongation)
							{
								res += res.Last();
							}
							else
							{
								if (isPreviousLtsu)
								{
									res += found.Romaji[0];
									isPreviousLtsu = false;
								}

								res += found.Romaji;
							}
						}
						else
						{
							if (i == reading.Length - 1 || kanas.Where(k => k.Character == $"{reading[i+1]}").First().DifficultyGroup != Difficulty.LittleCharsAfter)
							{
								Kana found = foundForChar.Where(k => k.Character.Length == 1).First();

								if (isPreviousLtsu)
								{
									res += found.Romaji[0];
									isPreviousLtsu = false;
								}

								res += found.Romaji;
							}
							else
							{
								Kana found = foundForChar.Where(k => k.Character.Contains($"{reading[i]}{reading[i+1]}")).First();

								if (isPreviousLtsu)
								{
									res += found.Romaji[0];
									isPreviousLtsu = false;
								}

								res += found.Romaji;

								i++;
							}
						}
					}
					else
					{
						res += reading[i];
					}

					if (res.Length > 1)
					{
						if (res[res.Length - 2] == res.Last())
						{
							res = res.Substring(0, res.Length - 2) + getMacron(res.Last());
						}
						else if (res.Substring(res.Length - 2) == "ou")
						{
							res = res.Substring(0, res.Length - 2) + 'ō';
						}
					}

					i++;
				}

				transcriptions.Add(reading, res);
			}

			return new Readings(transcriptions);
		}

		/// <summary>
		/// Gets the macron character corresponding to the provided character
		/// </summary>
		/// <param name="letter">The character</param>
		/// <returns>The corresponding character with a macron if it is a vowel, <paramref name="letter"/> otherwise</returns>
		private char getMacron(char letter)
		{
			switch (letter)
			{
				case 'a':
				case 'ā':
					return 'ā';
				case 'i':
				case 'ī':
					return 'ī';
				case 'u':
				case 'ū':
					return 'ū';
				case 'e':
				case 'ē':
					return 'ē';
				case 'o':
				case 'ō':
					return 'ō';
				default:
					return letter;
			}
		}

		/// <summary>
		/// Retrieves all <see cref="Kana"/> in word
		/// </summary>
		/// <param name="word">The <see cref="string"/> containing the <see cref="Kana"/> to fetch</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing the required <see cref="Kana"/></returns>
		private IEnumerable<Kana> GetInWord(string word)
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
