using AlbumLieux.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumLieux.Services
{
	public abstract class BaseDataService
	{
		private readonly string _baseAdress;

		/// <param name="baseAdress">Base address for http call</param>
		protected BaseDataService(string baseAdress)
		{
			_baseAdress = baseAdress;
		}

		/// <summary>
		/// Create an HttpClient with baseAddress from ctor
		/// </summary>
		/// <returns>HttpClient which need to be disposed</returns>
		protected HttpClient GetClient()
		{
			return new HttpClient()
			{
				BaseAddress = new Uri(_baseAdress)
			};
		}

		protected async Task<Response<T>> GetAsync<T>(string uri)
		{
			using (var client = GetClient())
			{
				var response = await client.GetStringAsync(uri);
				return JsonConvert.DeserializeObject<Response<T>>(response);
			}
		}
	}
}
