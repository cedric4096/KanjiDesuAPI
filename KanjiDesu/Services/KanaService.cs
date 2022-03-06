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
		/// <param name="exclusive">If <see langword="true"/>, returns kana stricly in the specified difficulty, else returns kana in surrounding groups (i.e. if <see cref="Difficulty.Dakuten"/> is provided, returns <see cref="Difficulty.ComposedDakuten"/> kanas too)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		public IEnumerable<Kana> Get(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return context.Kanas
				.Where(kana => (
						difficulty == null
						|| ((byte)difficulty == 0 && kana.DifficultyGroup == 0)
						|| ((kana.DifficultyGroup & (byte)difficulty) != 0)
						&& (
							((exclusive == null || !(bool)exclusive) && (Difficulty)kana.DifficultyGroup >= difficulty)
							|| (exclusive != null && (bool)exclusive && (Difficulty)kana.DifficultyGroup == difficulty)
						)
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
		/// <param name="exclusive">If <see langword="true"/>, returns hiragana stricly in the specified difficulty, else returns hiragana in surrounding groups (i.e. if <see cref="Difficulty.Dakuten"/> is provided, returns <see cref="Difficulty.ComposedDakuten"/> hiraganas too)</param>
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
		/// <param name="exclusive">If <see langword="true"/>, returns latakana stricly in the specified difficulty, else returns katakana in surrounding groups (i.e. if <see cref="Difficulty.Dakuten"/> is provided, returns <see cref="Difficulty.ComposedDakuten"/> katakanas too)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		public IEnumerable<Kana> GetKatakana(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return Get(difficulty, search, exclusive).Where(kana => !kana.IsHiragana);
		}
	}
}
