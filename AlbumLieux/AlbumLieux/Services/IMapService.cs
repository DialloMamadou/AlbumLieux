using System.Threading.Tasks;

namespace AlbumLieux.Services
{
	public interface IMapService
    {
		//DOCUMENTATION
		//https://developers.google.com/maps/documentation/urls/android-intents
		//https://developer.apple.com/documentation/mapkit/mkmapitem
		//https://docs.microsoft.com/en-us/dotnet/api/MapKit.MKMapItem
		//https://docs.microsoft.com/en-us/windows/uwp/launch-resume/launch-maps-app
		//sinon voir xamarin essentials
		Task ShowMap(double latitude, double longitude, string name);
    }
}
