using KanjiDesu.Helpers;
using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Interface for <see cref="Kana"/> retrieval
	/// </summary>
	public interface IKanaService
	{
		public IEnumerable<Kana> Get(Difficulty? difficulty, string? search, bool? exclusive);
		public IEnumerable<Kana> GetHiragana(Difficulty? difficulty, string? search, bool? exclusive);
		public IEnumerable<Kana> GetKatakana(Difficulty? difficulty, string? search, bool? exclusive);
	}
}
