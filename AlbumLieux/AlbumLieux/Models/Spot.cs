using Newtonsoft.Json;

namespace AlbumLieux
{
	public class Spot
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("image")]
		public string ImageUrl { get; set; }

		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		public int Index { get; set; }
	}
}
