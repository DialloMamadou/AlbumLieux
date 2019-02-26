using AlbumLieux.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AlbumLieux.Services
{
	public interface IPlacesDataServices
	{
		Task<List<Places>> ListPlaces();

		Task<Places> GetPlace(uint id);
	}

	public class PlacesDataServices : BaseDataService, IPlacesDataServices
	{
		public PlacesDataServices() : base("https://td-api.julienmialon.com/") { }

		public async Task<Places> GetPlace(uint id)
		{
			var response = await GetAsync<Places>($"places/{id}");
			if (response.IsSuccess)
			{
				return response.Data;
			}
			else
			{
				//TODO : throw special exception ?
				return null;
			}
		}
		
		public async Task<List<Places>> ListPlaces()
		{
			var response = await GetAsync<List<Places>>("places");
			if (response.IsSuccess)
			{
				return response.Data;
			}
			else
			{
				return null;
			}
		}
	}
}
