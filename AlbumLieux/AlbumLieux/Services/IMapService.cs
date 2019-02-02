using System.Threading.Tasks;

namespace AlbumLieux.Services
{
	public interface IMapService
    {
		Task ShowMap(double latitude, double longitude, string name);
    }
}
