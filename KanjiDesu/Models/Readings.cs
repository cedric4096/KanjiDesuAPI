using KanjiDesu.DataModels;

namespace KanjiDesu.Models
{
	public class Readings
	{
		public List<KeyValuePair<string, string>> ReadingsTranscriptions { get; private set; }

		public Readings(List<KeyValuePair<string, string>> readingTranscriptions)
		{
			ReadingsTranscriptions = readingTranscriptions;
		}
	}
}