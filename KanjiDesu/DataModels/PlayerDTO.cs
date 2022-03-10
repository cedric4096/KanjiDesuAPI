namespace KanjiDesu.DataModels
{
	public partial class PlayerDTO
	{
        public int Id { get; set; }
        public string Pseudo { get; set; } = null!;
        public int? BestScore { get; set; }
    }
}
