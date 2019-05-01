using Newtonsoft.Json;

namespace AlbumLieux.Models
{
    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("image_id")]
        public int? ImageId { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string ImageUrl => $"{Constants.BASE_URL}/images/{ImageId}";
    }
}
