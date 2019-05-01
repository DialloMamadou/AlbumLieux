using Newtonsoft.Json;

namespace AlbumLieux.Models.Requests
{
    public class PostCommentRequest
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
