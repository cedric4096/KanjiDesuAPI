using KanjiDesu.Models;
using KanjiDesu.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanjiDesu.Controllers
{
	/// <summary>
	/// Controller for <see cref="Kanji"/> management
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class KanjiController : ControllerBase
	{
		private readonly ILogger<KanjiController> logger;
		private readonly IKanjiService kanjiService;

		public KanjiController(ILogger<KanjiController> logger, IKanjiService kanjiService)
		{
			this.logger = logger;
			this.kanjiService = kanjiService;
		}

		/// <summary>
		/// Returns the first 10 existing kanji
		/// </summary>
		/// <param name="jlpt">JLPT level of the searched kanji</param>
		/// <param name="exclusive">If true, returns kanji stricly in the specified JLPT level, else returns kanji in easier levels too</param>
		/// <param name="page">The page of kanji to load</param>
		/// <param name="kanjiPerPage">The number of kanji per page, 10 by default</param>
		/// <remarks>Pagination is based on the results of the jlpt and exclusive arguments, thus pagination may not remain consistent with different parameters</remarks>
		/// <returns>An IEnumerable{T} containing searched Kanji</returns>
		/// <response code="200">Returns the searched kanji</response>
		/// <response code="400">If JLPT level is invalid, or if JLPT level is null and exclusive is provided</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kanji>> GetAll([FromQuery] byte? jlpt, [FromQuery] bool? exclusive, [FromQuery] int page, [FromQuery] int kanjiPerPage)
		{
			if (jlpt < 0 || jlpt > 5)
			{
				return BadRequest("Invalid JLPT level");
			}
			if (exclusive != null && jlpt == null)
			{
				return BadRequest("'exclusive' field used without a dificulty group");
			}

			return Ok(kanjiService.Get(jlpt, exclusive, page, kanjiPerPage));
		}

		/// <summary>
		/// Returns kanji corresponding to the given meaning
		/// </summary>
		/// <param name="meaning">Meaning of the searched kanji</param>
		/// <returns>An IEnumerable{T} containing searched Kanji</returns>
		/// <response code="200">Returns the searched kanji</response>
		[HttpGet("meaning/{meaning}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Kanji>> GetByMeaning(string meaning)
		{
			return Ok(kanjiService.GetByMeaning(meaning));
		}
	}
}
