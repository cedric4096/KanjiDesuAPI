namespace KanjiDesu.DataModels
{
    public partial class KanaDTO
    {
        public int Id { get; set; }
        public string Utf { get; set; } = null!;
        public string Kana { get; set; } = null!;
        public string Romaji { get; set; } = null!;
        public byte DifficultyGroup { get; set; }
        public bool IsHiragana { get; set; }
    }
}
