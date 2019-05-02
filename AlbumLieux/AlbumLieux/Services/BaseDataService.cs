using AlbumLieux.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlbumLieux.Services
{
    public abstract class BaseDataService
    {
        private readonly Lazy<ITokenService> _tokenService;

        protected BaseDataService()
        {
            _tokenService = new Lazy<ITokenService>(() => DependencyService.Resolve<ITokenService>());
        }

        protected async Task<Response<T>> GetAsync<T>(string uri, bool authenticated = false)
        {
            var msg = new HttpRequestMessage(HttpMethod.Get, uri);
            if (authenticated)
            {
                await SetAuthenticationToken(msg);
            }
            var response = await SendRequest(msg);
            var contentResponse = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<T>>(contentResponse);
        }

        protected async Task<Response> PostAsync<TRequest>(string uri, TRequest data, bool authenticated = false)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri);
            var body = JsonConvert.SerializeObject(data).ToStringContent();
            msg.Content = body;
            if (authenticated)
            {
                await SetAuthenticationToken(msg);
            }

            var response = await SendRequest(msg);
            var contentResponse = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response>(contentResponse);
        }

        protected async Task<Response<TResult>> PostAsync<TResult, TRequest>(string uri, TRequest data, bool authenticated = false)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri);
            var body = JsonConvert.SerializeObject(data).ToStringContent();
            msg.Content = body;
            if (authenticated)
            {
                await SetAuthenticationToken(msg);
            }

            var response = await SendRequest(msg);
            var contentResponse = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<TResult>>(contentResponse);
        }

        protected async Task<Response<TResult>> PostAsync<TResult>(string uri, HttpContent body, bool authenticated = false)
        {
            var msg = new HttpRequestMessage(HttpMethod.Post, uri);
            msg.Content = body;
            if (authenticated)
            {
                await SetAuthenticationToken(msg);
            }

            var response = await SendRequest(msg);
            var contentResponse = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<TResult>>(contentResponse);
        }

        protected async Task<Response<TResult>> PatchAsync<TResult, TRequest>(string uri, TRequest data, bool authenticated = false)
        {
            var msg = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
            var body = JsonConvert.SerializeObject(data).ToStringContent();
            msg.Content = body;
            if (authenticated)
            {
                await SetAuthenticationToken(msg);
            }

            var response = await SendRequest(msg);
            var contentResponse = await response.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<TResult>>(contentResponse);
        }

        private async Task SetAuthenticationToken(HttpRequestMessage msg)
        {
            var token = _tokenService.Value.GetToken();
            if (token is null || token.ExpiresAt < (DateTime.Now - TimeSpan.FromSeconds(30)))
            {
                await _tokenService.Value.Refresh();
                token = _tokenService.Value.GetToken();
            }

            msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        private async Task<HttpContent> SendRequest(HttpRequestMessage msg)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Constants.BASE_URL)
            };

            var response = await client.SendAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
