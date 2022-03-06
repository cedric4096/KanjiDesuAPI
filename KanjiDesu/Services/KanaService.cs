using KanjiDesu.DataModels;
using KanjiDesu.Helpers;
using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	public class KanaService : IKanaService
	{
		private readonly KanjiDesuContext context;

		public KanaService(KanjiDesuContext context)
		{
			this.context = context;
		}

		public IEnumerable<Kana> Get(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return context.Kanas
				.Where(kana => (difficulty == null || ((byte)difficulty == 0 && kana.DifficultyGroup == 0) || (
						(kana.DifficultyGroup & (byte)difficulty) != 0)
						&& (Difficulty)kana.DifficultyGroup >= difficulty
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

		public IEnumerable<Kana> GetHiragana(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return Get(difficulty, search, exclusive).Where(kana => kana.IsHiragana);
		}

		public IEnumerable<Kana> GetKatakana(Difficulty? difficulty, string? search, bool? exclusive)
		{
			return Get(difficulty, search, exclusive).Where(kana => !kana.IsHiragana);
		}
	}
}
