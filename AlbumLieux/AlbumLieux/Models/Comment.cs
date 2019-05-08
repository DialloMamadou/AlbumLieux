using Newtonsoft.Json;
using System;

namespace AlbumLieux.Models
{
	public class Comment
	{
		[JsonProperty("author")]
		public User Author { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("text")]
		public string Content { get; set; }
	}
}
