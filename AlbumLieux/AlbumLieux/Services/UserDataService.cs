using AlbumLieux.Exceptions;
using AlbumLieux.Models;
using AlbumLieux.Models.Requests;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlbumLieux.Services
{
	public interface IUserDataService
	{
		Task Connect(string mail, string password);
		Task Register(string mail, string firstname, string lastname, string password);
		Task Refresh(string refreshToken);
	}

	public class UserDataService : BaseDataService, IUserDataService
	{
		private readonly Lazy<ITokenService> _tokenService;

		public UserDataService()
		{
			_tokenService = new Lazy<ITokenService>(() => DependencyService.Resolve<ITokenService>());
		}

		public async Task Connect(string mail, string password)
		{
			var response = await PostAsync<Token, LoginRequest>("/auth/login", new LoginRequest
			{
				Email = mail,
				Password = password
			});

			if (response.IsSuccess)
			{
				_tokenService.Value.AddToken(response.Data);
			}
			else
			{
				throw new ApiException(response.ErrorCode, response.ErrorMessage);
			}
		}

		public async Task Refresh(string refreshToken)
		{
			var response = await PostAsync<Token, RefreshRequest>("/auth/refresh", new RefreshRequest
			{
				RefreshToken = refreshToken
			});

			if (response.IsSuccess)
			{
				_tokenService.Value.AddToken(response.Data);
			}
			else
			{
				throw new ApiException(response.ErrorCode, response.ErrorMessage);
			}
		}

		public async Task Register(string mail, string firstname, string lastname, string password)
		{
			var response = await PostAsync<Token, RegisterRequest>("/auth/register", new RegisterRequest
			{
				Email = mail,
				FirstName = firstname,
				LastName = lastname,
				Password = password
			});

			if (response.IsSuccess)
			{
				_tokenService.Value.AddToken(response.Data);
			}
			else
			{
				throw new ApiException(response.ErrorCode, response.ErrorMessage);
			}
		}
	}
}
