using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Interface for <see cref="Kanji"/> retrieval
	/// </summary>
	public interface IKanjiService
	{
		public IEnumerable<Kanji> Get(byte? jlpt, bool? exclusive);
		public IEnumerable<Kanji> GetByReading(string romaji);
		public IEnumerable<Kanji> GetByMeaning(string meaning);
		public Kanji Test();
	}
}
