using Newtonsoft.Json;
using System;

namespace AlbumLieux.Models
{
	public class Comment
	{
		[JsonProperty("author_name")]
		public string Author { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("text")]
		public string Content { get; set; }
	}
}
