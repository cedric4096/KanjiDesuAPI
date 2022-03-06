using KanjiDesu.Helpers;
using KanjiDesu.Models;
using KanjiDesu.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanjiDesu.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
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
		/// <param name="difficulty"><see cref="Difficulty"/> of the searched kana</param>
		/// <param name="search">Romaji <see cref="string"/> to search in kana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns kana stricly in the specified difficulty, else returns kana in surrounding groups (i.e. if <see cref="Difficulty.Dakuten"/> is provided, returns <see cref="Difficulty.ComposedDakuten"/> kanas too)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		/// <response code="200">Returns the searched kana</response>
		/// <response code="400">If <paramref name="difficulty"/> is invalid, or if <paramref name="difficulty"/> is <see langword="null"/> and <paramref name="exclusive"/> is provided</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kana>> GetAll([FromQuery] Difficulty? difficulty, [FromQuery] string? search, [FromQuery] bool? exclusive)
		{
			if (difficulty != null && difficulty < Difficulty.Simple || difficulty > Difficulty.ComposedDakuten)
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
		/// <param name="difficulty"><see cref="Difficulty"/> of the searched hiragana</param>
		/// <param name="search">Romaji <see cref="string"/> to search in hiragana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns hiragana stricly in the specified difficulty, else returns hiragana in surrounding groups (i.e. if <see cref="Difficulty.Dakuten"/> is provided, returns <see cref="Difficulty.ComposedDakuten"/> hiraganas too)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		/// <response code="200">Returns the searched hiragana</response>
		/// <response code="400">If <paramref name="difficulty"/> is invalid, or if <paramref name="difficulty"/> is <see langword="null"/> and <paramref name="exclusive"/> is provided</response>
		[HttpGet("hiragana")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kana>> GetHiragana([FromQuery] Difficulty? difficulty, [FromQuery] string? search, [FromQuery] bool? exclusive)
		{
			if (difficulty != null && difficulty < Difficulty.Simple || difficulty > Difficulty.ComposedDakuten)
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
		/// <param name="difficulty"><see cref="Difficulty"/> of the searched katakana</param>
		/// <param name="search">Romaji <see cref="string"/> to search in katakana</param>
		/// <param name="exclusive">If <see langword="true"/>, returns latakana stricly in the specified difficulty, else returns katakana in surrounding groups (i.e. if <see cref="Difficulty.Dakuten"/> is provided, returns <see cref="Difficulty.ComposedDakuten"/> katakanas too)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing searched <see cref="Kana"/></returns>
		/// <response code="200">Returns the searched katakana</response>
		/// <response code="400">If <paramref name="difficulty"/> is invalid, or if <paramref name="difficulty"/> is <see langword="null"/> and <paramref name="exclusive"/> is provided</response>
		[HttpGet("katakana")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<IEnumerable<Kana>> GetKatakana([FromQuery] Difficulty? difficulty, [FromQuery] string? search, [FromQuery] bool? exclusive)
		{
			if (difficulty != null && difficulty < Difficulty.Simple || difficulty > Difficulty.ComposedDakuten)
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
