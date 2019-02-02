using AlbumLieux.Services;
using Storm.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private string _email;

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		private string _password;

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		public ICommand ConnectCommand { get; }

		public LoginViewModel()
		{
			ConnectCommand = new Command(ConnectAction);
		}

		private async void ConnectAction(object obj)
		{
			var connectService = DependencyService.Get<IConnectedUserService>();
			await connectService.Connect(Email, Password);
			await NavigationService.PopAsync();
		}
	}
}
