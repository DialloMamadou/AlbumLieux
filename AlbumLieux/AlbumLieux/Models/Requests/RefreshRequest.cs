using Newtonsoft.Json;

namespace AlbumLieux.Models.Requests
{
    public class RefreshRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
