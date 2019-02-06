using System;
using System.Threading.Tasks;
using AlbumLieux.Exceptions;

namespace AlbumLieux.Services
{
	public interface IConnectedUserService
	{
		bool IsConnected { get; }
		string CurrentUserName { get; }
		Task Connect(string username, string password);
		Task Disconnect();
	}

	public class ConnectedUserService : IConnectedUserService
	{
		public bool IsConnected { get; set; }

		public string CurrentUserName { get; set; }

		public Task Connect(string username, string password)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new EmptyFieldException("username");
			}
			else if (string.IsNullOrEmpty(password))
			{
				throw new EmptyFieldException("password");
			}
			else
			{
				IsConnected = true;
				CurrentUserName = username.Split('@')[0];

				//TODO : use webservice
			}

			return Task.CompletedTask;
		}

		public Task Disconnect()
		{
			IsConnected = false;
			CurrentUserName = string.Empty;

			//TODO use webservice

			return Task.CompletedTask;
		}
	}
}
