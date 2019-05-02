using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace AlbumLieux.Models
{
	public class Places
	{
		[JsonProperty("id")]
		public uint Id { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("image_id")]
		public uint ImageId { get; set; }

		public string ImageUrl => $"{Constants.BASE_URL}/images/{ImageId}";

		[JsonProperty("latitude")]
		public double Latitude { get; set; }

		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		[JsonProperty("comments")]
		public List<Comment> CommentList { get; set; }

		[JsonIgnore]
		public double DistanceToMe { get; set; }
	}
}
