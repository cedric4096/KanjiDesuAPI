using KanjiDesu.Models;

namespace KanjiDesu.Services
{
	/// <summary>
	/// Interface for <see cref="Player"/> retrieval
	/// </summary>
	public interface IPlayerService
	{
		public Player Get(string pseudo);
		public Player Create(string pseudo, int? bestScore);
		public Player Update(string pseudo, int? bestScore);
		public void Delete(string pseudo);
	}
}
