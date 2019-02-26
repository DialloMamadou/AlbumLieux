using Newtonsoft.Json;

namespace AlbumLieux.Models
{
	public class Response<T>
    {
		[JsonProperty("is_success")]
		public bool IsSuccess { get; set; }

		[JsonProperty("error_code")]
		public string ErrorCode { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		[JsonProperty("data")]
		public T Data { get; set; }
	}
}
