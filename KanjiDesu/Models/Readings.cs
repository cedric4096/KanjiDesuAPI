namespace KanjiDesu.Models
{
	/// <summary>
	/// Represents the different readings of a <see cref="Kanji"/>
	/// </summary>
	public class Readings
	{
		/// <summary>
		/// Gets the <see cref="Dictionary{TKey, TValue}"/> of readings, the keys being the kana words and their associated values the corresponding rōmaji transcription
		/// </summary>
		public Dictionary<string, string> ReadingsTranscriptions { get; private set; }

		public Readings(Dictionary<string, string> readingTranscriptions)
		{
			ReadingsTranscriptions = readingTranscriptions;
		}
	}
}