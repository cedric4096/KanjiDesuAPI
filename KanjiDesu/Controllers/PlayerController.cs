using KanjiDesu.Models;
using KanjiDesu.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanjiDesu.Controllers
{
	/// <summary>
	/// Controller for <see cref="Player"/> management
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class PlayerController : ControllerBase
	{
		private readonly ILogger<PlayerController> logger;
		private readonly IPlayerService playerService;

		public PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
		{
			this.logger = logger;
			this.playerService = playerService;
		}

		/// <summary>
		/// Returns the player with the provided pseudonym
		/// </summary>
		/// <param name="pseudo">The pseudonym of the player to retrieve</param>
		/// <returns>The player with the provided pseudonym</returns>
		/// <response code="200">If the player exists</response>
		/// <response code="404">If the player was not found</response>
		[HttpGet("{pseudo}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Player> Get(string pseudo)
		{
			try
			{
				return Ok(playerService.Get(pseudo));
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}

		}

		/// <summary>
		/// Creates and returns a new player
		/// </summary>
		/// <param name="pseudo">The unique pseudonym for the new player</param>
		/// <param name="bestScore">The best score for the new player</param>
		/// <returns>The created player</returns>
		/// <response code="201">Returns the created player</response>
		/// <response code="400">If the pseudonym already exists</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult<Player> Post([FromQuery] string pseudo, [FromQuery] int? bestScore)
		{
			try
			{
				return Created($"/api/player/{pseudo}", playerService.Create(pseudo, bestScore));
			}
			catch (ArgumentException)
			{
				return BadRequest();
			}
		}

		/// <summary>
		/// Updates and returns the player with the provided pseudonym
		/// </summary>
		/// <param name="pseudo">The pseudonym of the player to update</param>
		/// <param name="bestScore">The new best score for the player</param>
		/// <returns>The updated player</returns>
		/// <response code="202">If the player exists</response>
		/// <response code="404">If the player was not found</response>
		[HttpPut("{pseudo}")]
		[ProducesResponseType(StatusCodes.Status202Accepted)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Player> Put(string pseudo, [FromQuery] int? bestScore)
		{
			try
			{
				return Accepted(playerService.Update(pseudo, bestScore));
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}

		/// <summary>
		/// Deletes a player
		/// </summary>
		/// <param name="pseudo">The pseudonym of the player to delete</param>
		/// <response code="200">The player is deleted</response>
		/// <response code="404">If the pseudonym was not found</response>
		[HttpDelete("{pseudo}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult Delete(string pseudo)
		{
			try
			{
				playerService.Delete(pseudo);
				return Ok();
			}
			catch (KeyNotFoundException)
			{
				return NotFound();
			}
		}
	}
}
