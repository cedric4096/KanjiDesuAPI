namespace KanjiDesu.Models
{
	public class Readings
	{
		public Dictionary<string, string> ReadingsTranscriptions { get; private set; }

		public Readings(Dictionary<string, string> readingTranscriptions)
		{
			ReadingsTranscriptions = readingTranscriptions;
		}
	}
}