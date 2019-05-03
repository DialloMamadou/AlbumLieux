using Newtonsoft.Json;

namespace AlbumLieux.Models.Requests
{
	public class AddPlaceRequest
	{
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("image_id")]
		public int ImageId { get; set; }

		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }
	}
}
