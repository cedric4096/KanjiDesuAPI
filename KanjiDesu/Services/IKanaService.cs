using KanjiDesu.Helpers;
using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	public interface IKanaService
	{
		public IEnumerable<Kana> Get(Difficulty? difficulty, string? search, bool? exclusive);
		public IEnumerable<Kana> GetHiragana(Difficulty? difficulty, string? search, bool? exclusive);
		public IEnumerable<Kana> GetKatakana(Difficulty? difficulty, string? search, bool? exclusive);
	}
}
