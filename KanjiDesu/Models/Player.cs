namespace KanjiDesu.Models
{
	/// <summary>
	/// Represents a KanjiDesu player
	/// </summary>
	public class Player
	{
		/// <summary>
		/// Gets or sets the player's pseudonym for this instance
		/// </summary>
		public string Pseudo { get; set; }
		/// <summary>
		/// Gets or sets the player's best score
		/// </summary>
		public int? BestScore { get; set; } = null!;

		public Player(string pseudo, int bestScore)
		{
			Pseudo = pseudo;
			BestScore = bestScore;
		}
	}
}
