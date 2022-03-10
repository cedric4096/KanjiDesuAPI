using KanjiDesu.DataModels;
using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Service for <see cref="Player"/> retrieval from DB
	/// </summary>
	public class PlayerService : IPlayerService
	{
		private readonly KanjiDesuContext context;

		public PlayerService(KanjiDesuContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Creates and returns a new <see cref="Player"/>
		/// </summary>
		/// <param name="pseudo">The unique pseudonym for the new <see cref="Player"/></param>
		/// <param name="bestScore">The best score for the new <see cref="Player"/></param>
		/// <returns>The created <see cref="Player"/></returns>
		/// <exception cref="ArgumentException"/>
		public async Task<Player> Create(string pseudo, int? bestScore)
		{
			if (context.Players.Where(player => player.Pseudo == pseudo).Count() != 0)
			{
				throw new ArgumentException("The pseudonym already exists");
			}

			context.Players.Add(new PlayerDTO() { Pseudo = pseudo, BestScore = bestScore });
			await context.SaveChangesAsync();

			return Get(pseudo);
		}

		/// <summary>
		/// Deletes a <see cref="Player"/>
		/// </summary>
		/// <param name="pseudo">The pseudonym of the <see cref="Player"/> to delete</param>
		/// <exception cref="KeyNotFoundException"/>
		public async Task Delete(string pseudo)
		{
			try
			{
				context.Players.Remove(
					context.Players
						.Where(player => player.Pseudo == pseudo)
						.First()
				);
				await context.SaveChangesAsync();
			}
			catch (InvalidOperationException)
			{
				throw new KeyNotFoundException("No player with the provided pseudonym was found");
			}
		}

		/// <summary>
		/// Returns the <see cref="Player"/> with the provided pseudonym
		/// </summary>
		/// <param name="pseudo">The pseudonym of the <see cref="Player"/> to retrieve</param>
		/// <returns>The <see cref="Player"/> with the provided pseudonym</returns>
		/// <exception cref="KeyNotFoundException"/>
		public Player Get(string pseudo)
		{
			try
			{
				return context.Players
					.Where(player => player.Pseudo == pseudo)
					.Select(dto => new Player(dto.Pseudo, dto.BestScore != null ? (int)dto.BestScore : 0))
					.First();
			}
			catch (InvalidOperationException)
			{
				throw new KeyNotFoundException("No player with the provided pseudonym was found");
			}
		}

		/// <summary>
		/// Updates and returns the <see cref="Player"/> with the provided pseudonym
		/// </summary>
		/// <param name="pseudo">The pseudonym of the <see cref="Player"/> to update</param>
		/// <param name="bestScore">The new best score for the <see cref="Player"/></param>
		/// <returns>The updated <see cref="Player"/></returns>
		/// <exception cref="KeyNotFoundException"/>
		public async Task<Player> Update(string pseudo, int? bestScore)
		{
			try
			{
				context.Players
					.Where(player => player.Pseudo == pseudo)
					.First()
					.BestScore = bestScore;
				await context.SaveChangesAsync();

				return Get(pseudo);
			}
			catch (InvalidOperationException)
			{
				throw new KeyNotFoundException("No player with the provided pseudonym was found");
			}
		}
	}
}
