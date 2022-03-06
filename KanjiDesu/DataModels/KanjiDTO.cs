namespace KanjiDesu.DataModels
{
	public partial class KanjiDTO
	{
		public int Id { get; set; }
		public string Utf { get; set; } = null!;
		public string Kanji { get; set; } = null!;
		public byte? Jlpt { get; set; }
		public string? Meanings { get; set; }
		public string? MeaningsFr { get; set; }
		public string? OnReadings { get; set; }
		public string? KunReadings { get; set; }
	}
}
