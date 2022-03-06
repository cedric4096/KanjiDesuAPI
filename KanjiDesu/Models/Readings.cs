namespace KanjiDesu.Models
{
	public class Readings
	{
		public List<string> KanaReadings { get; set; }
		public List<string> RomajiReadings { get; set; }

		public Readings()
		{
			KanaReadings = new List<string>();
			RomajiReadings = new List<string>();
		}
	}
}