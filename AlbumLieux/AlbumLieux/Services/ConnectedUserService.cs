using System;
using System.Threading.Tasks;

namespace AlbumLieux.Services
{
	public interface IConnectedUserService
	{
		bool IsConnected { get; }
		string CurrentUserName { get; }
		Task Connect(string email, string password);
		Task Disconnect();
	}

	public class ConnectedUserService : IConnectedUserService
	{
		public bool IsConnected { get; set; }

		public string CurrentUserName { get; set; }

		public Task Connect(string email, string password)
		{
			if (string.IsNullOrEmpty(email))
			{
				//TODO WARNING
			}
			else if (string.IsNullOrEmpty(password))
			{
				//TODO WARNING
			}
			else
			{
				IsConnected = true;
				CurrentUserName = email.Split('@')[0];

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
