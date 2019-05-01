using AlbumLieux.Models;
using AlbumLieux.Models.Requests;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlbumLieux.Services
{
    public interface IPlacesDataServices
    {
        Task<List<Places>> ListPlaces(bool force = false);

        Task<Places> GetPlace(uint id);

        Task PostComment(uint id, string text);
    }

    public class PlacesDataServices : BaseDataService, IPlacesDataServices
    {
        private const string CACHE_LIST_KEY = "PLACES_KEY";

        public async Task<Places> GetPlace(uint id)
        {
            Response<Places> response = await GetAsync<Places>($"places/{id}");
            if (response.IsSuccess)
            {
                return response.Data;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Places>> ListPlaces(bool force = false)
        {
            if (!force && !Barrel.Current.IsExpired(CACHE_LIST_KEY))
            {
                return Barrel.Current.Get<List<Places>>(CACHE_LIST_KEY);
            }
            else
            {
                var response = await GetAsync<List<Places>>("places");
                if (response.IsSuccess)
                {
                    Barrel.Current.Add(CACHE_LIST_KEY, response.Data, TimeSpan.FromHours(3));
                    return response.Data;
                }
                else
                {
                    return null;
                }
            }
        }

        public Task PostComment(uint id, string text)
        {
            return PostAsync($"places/{id}/comments", new PostCommentRequest
            {
                Text = text
            }, authenticated: true);
        }
    }
}
