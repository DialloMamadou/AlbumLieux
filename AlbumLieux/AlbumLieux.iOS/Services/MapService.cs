using System.Threading.Tasks;
using AlbumLieux.Services;
using CoreLocation;
using Foundation;
using MapKit;

namespace AlbumLieux.iOS.Services
{
	public class MapService : IMapService
	{
		public Task ShowMap(double latitude, double longitude, string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				name = string.Empty;
			}

			NSDictionary dictionary = null;
			var placemark = new MKPlacemark(new CLLocationCoordinate2D(latitude, longitude), dictionary);

			var mapItem = new MKMapItem(placemark)
			{
				Name = name
			};

			MKLaunchOptions launchOptions = null;

			var mapItems = new[] { mapItem };
			MKMapItem.OpenMaps(mapItems, launchOptions);
			return Task.CompletedTask;
		}
	}
}