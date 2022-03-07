using KanjiDesu.Helpers;
using KanjiDesu.Models;
using KanjiDesu.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanjiDesu.Controllers
{
	/// <summary>
	/// Controller for <see cref="Kana"/> management
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class KanaController : ControllerBase
	{
		private readonly ILogger<KanaController> logger;
		private readonly IKanaService kanaService;

		public KanaController(ILogger<KanaController> logger, IKanaService kanaService)
		{
			this.logger = logger;
			this.kanaService = kanaService;
		}

		/// <summary>
		/// Returns all existing kana
		/// </summary>
		/// <param name="difficulty">Difficulty of the searched kana</param>
		/// <param name="search">Romaji string to search in kana</param>
		/// <param name="exclusive">If true, returns kana stricly in the specified difficulty, else returns kana in easier groups</param>
		/// <returns>An IEnumerable{T} containing searched Kana</returns>
		/// <response code="200">Returns the searched kana</response>
		/// <response code="400">If difficulty is invalid, or if difficulty is null and exclusive is provided</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kana>> GetAll([FromQuery] Difficulty? difficulty, [FromQuery] string? search, [FromQuery] bool? exclusive)
		{
			if (difficulty != null && difficulty < Difficulty.Simple || difficulty > Difficulty.Exotic)
			{
				return BadRequest("Invalid group identifier");
			}
			if (exclusive != null && difficulty == null)
			{
				return BadRequest("'exclusive' field used without a dificulty group");
			}

			return Ok(kanaService.Get(difficulty, search, exclusive));
		}

		/// <summary>
		/// Returns all existing hiragana
		/// </summary>
		/// <param name="difficulty">Difficulty of the searched hiragana</param>
		/// <param name="search">Romaji string to search in hiragana</param>
		/// <param name="exclusive">If true, returns hiragana stricly in the specified difficulty, else returns hiragana in easier groups</param>
		/// <returns>An IEnumerable{T} containing searched Kana</returns>
		/// <response code="200">Returns the searched hiragana</response>
		/// <response code="400">If difficulty is invalid, or if difficulty is null and exclusive is provided</response>
		[HttpGet("hiragana")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kana>> GetHiragana([FromQuery] Difficulty? difficulty, [FromQuery] string? search, [FromQuery] bool? exclusive)
		{
			if (difficulty != null && difficulty < Difficulty.Simple || difficulty > Difficulty.Exotic)
			{
				return BadRequest("Invalid group identifier");
			}
			if (exclusive != null && difficulty == null)
			{
				return BadRequest("'exclusive' field used without a dificulty group");
			}

			return Ok(kanaService.GetHiragana(difficulty, search, exclusive));
		}

		/// <summary>
		/// Returns all existing katakana
		/// </summary>
		/// <param name="difficulty">Difficulty of the searched katakana</param>
		/// <param name="search">Romaji string to search in katakana</param>
		/// <param name="exclusive">If true, returns latakana stricly in the specified difficulty, else returns katakana in easier groups</param>
		/// <returns>An IEnumerable{T} containing searched Kana</returns>
		/// <response code="200">Returns the searched katakana</response>
		/// <response code="400">If difficulty is invalid, or if difficulty is null and exclusive is provided</response>
		[HttpGet("katakana")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kana>> GetKatakana([FromQuery] Difficulty? difficulty, [FromQuery] string? search, [FromQuery] bool? exclusive)
		{
			if (difficulty != null && difficulty < Difficulty.Simple || difficulty > Difficulty.Exotic)
			{
				return BadRequest("Invalid group identifier");
			}
			if (exclusive != null && difficulty == null)
			{
				return BadRequest("'exclusive' field used without a dificulty group");
			}

			return Ok(kanaService.GetKatakana(difficulty, search, exclusive));
		}
	}
}
