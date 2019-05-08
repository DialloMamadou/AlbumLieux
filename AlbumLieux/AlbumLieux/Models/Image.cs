using Newtonsoft.Json;

namespace AlbumLieux.Models
{
	public class Image
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		public string Url => $"{Constants.BASE_URL}/images/{Id}";
	}
}
