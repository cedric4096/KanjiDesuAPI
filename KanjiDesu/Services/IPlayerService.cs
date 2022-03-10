using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Interface for <see cref="Player"/> retrieval
	/// </summary>
	public interface IPlayerService
	{
		public Player Get(string pseudo);
		public Task<Player> Create(string pseudo, int? bestScore);
		public Task<Player> Update(string pseudo, int? bestScore);
		public Task Delete(string pseudo);
	}
}
