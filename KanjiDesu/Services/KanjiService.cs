using KanjiDesu.DataModels;
using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Service for <see cref="Kanji"/> retrieval from DB
	/// </summary>
	public class KanjiService : IKanjiService
	{
		private readonly KanjiDesuContext context;
		private readonly IKanaService kanaService;

		public KanjiService(KanjiDesuContext context, IKanaService kanaService)
		{
			this.context = context;
			this.kanaService = kanaService;
		}

		/// <summary>
		/// Retrieves all kanji from DB
		/// </summary>
		/// <param name="jlpt">JLPT level of the searched kanji</param>
		/// <param name="exclusive">If <see langword="true"/>, returns kanji stricly in the specified JLPT level, else returns kanji in easier levels too</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kanji"/></returns>
		public IEnumerable<Kanji> Get(byte? jlpt, bool? exclusive)
		{
			return context.Kanjis
				.Where(kanji => 
					jlpt == null
					|| ((exclusive == null || !(bool)exclusive) && kanji.Jlpt >= jlpt)
					|| (exclusive != null && (bool)exclusive && kanji.Jlpt == jlpt)
				)
				.Select(kanji => new Kanji(kanji, kanaService.BuildReadings(kanji.OnReadings), kanaService.BuildReadings(kanji.KunReadings)));
		}

		public IEnumerable<Kanji> GetByMeaning(string meaning)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Kanji> GetByReading(string romaji)
		{
			throw new NotImplementedException();
		}
	}
}
