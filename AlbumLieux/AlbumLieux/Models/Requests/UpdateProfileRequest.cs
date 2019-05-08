using Newtonsoft.Json;

namespace AlbumLieux.Models.Requests
{
	class UpdateProfileRequest
	{
		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("image_id")]
		public int ImageId { get; set; }
	}
}
