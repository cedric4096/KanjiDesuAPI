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
		/// Retrieves the first 10 <see cref="Kanji"/> from DB
		/// </summary>
		/// <param name="jlpt">JLPT level of the searched <see cref="Kanji"/></param>
		/// <param name="exclusive">If <see langword="true"/>, returns <see cref="Kanji"/> stricly in the specified JLPT level, else returns <see cref="Kanji"/> in easier levels too</param>
		/// <param name="page">The page of <see cref="Kanji"/> to load from DB</param>
		/// <param name="kanjiPerPage">The number of <see cref="Kanji"/> per page, 10 by default</param>
		/// <remarks>Pagination is based on the results of the <paramref name="jlpt"/> and <paramref name="exclusive"/> arguments, thus pagination may not remain consistent with different parameters</remarks>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kanji"/></returns>
		public IEnumerable<Kanji> Get(byte? jlpt, bool? exclusive, int? page, int? kanjiPerPage)
		{
			List<KanjiDTO> kanjis = context.Kanjis
				.Where(kanji =>
					jlpt == null
					|| ((exclusive == null || !(bool)exclusive) && kanji.Jlpt >= jlpt)
					|| (exclusive != null && (bool)exclusive && kanji.Jlpt == jlpt)
				).OrderBy(kanji => kanji.Id)
				.Skip(((int)kanjiPerPage != 0 ? (int)kanjiPerPage : 10) * (int)page)
				.Take((int)kanjiPerPage != 0 ? (int)kanjiPerPage : 10)
				.ToList();
				
			return kanjis.Select(kanji => new Kanji(kanji, kanaService.BuildReadings(kanji.OnReadings), kanaService.BuildReadings(kanji.KunReadings)));
		}

		/// <summary>
		/// Returns <see cref="Kanji"/> corresponding to the given meaning
		/// </summary>
		/// <param name="meaning">Meaning of the searched <see cref="Kanji"/></param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kanji"/></returns>
		public IEnumerable<Kanji> GetByMeaning(string meaning)
		{
			List<KanjiDTO> kanjis = context.Kanjis
				.Where(kanji => 
					kanji.Meanings.Contains(meaning)
					|| (!string.IsNullOrEmpty(kanji.MeaningsFr) && kanji.MeaningsFr.Contains(meaning))
				).ToList();

			return kanjis.Select(kanji => new Kanji(kanji, kanaService.BuildReadings(kanji.OnReadings), kanaService.BuildReadings(kanji.KunReadings)));
		}
	}
}
