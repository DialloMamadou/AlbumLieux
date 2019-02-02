
using AlbumLieux.Services;
using Android.App;
using Android.Content;
using Android.Net;
using System.Threading.Tasks;

namespace AlbumLieux.Droid.Services
{
	public class MapService : IMapService
	{
		public Task ShowMap(double latitude, double longitude, string name)
		{
			var uri = $"geo:{latitude},{longitude}?q={latitude},{longitude}";

			if (!string.IsNullOrWhiteSpace(name))
			{
				uri += $"({Uri.Encode(name)})";
			}

			var intent = new Intent(Intent.ActionView, Uri.Parse(uri));
			intent.SetFlags(ActivityFlags.ClearTop);
			intent.SetFlags(ActivityFlags.NewTask);

			Application.Context.StartActivity(intent);

			return Task.CompletedTask;
		}
	}
}