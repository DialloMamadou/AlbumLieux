using AlbumLieux.Services;
using System;
using System.Threading.Tasks;

namespace AlbumLieux.UWP.Services
{
	public class MapService : IMapService
	{
		public Task ShowMap(double latitude, double longitude, string name)
		{
			var uri = $"bingmaps:?collection=point.{latitude}_{longitude}_{name}";

			return Windows.System.Launcher.LaunchUriAsync(new Uri(uri)).AsTask();
		}
	}
}
