using Newtonsoft.Json;

namespace AlbumLieux.Models.Requests
{
	public class LoginRequest
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}
}
