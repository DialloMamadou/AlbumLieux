using AlbumLieux.Services;
using Storm.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using AlbumLieux.Exceptions;

namespace AlbumLieux.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private string _username;
		public string Username
		{
			get => _username;
			set
			{
				SetProperty(ref _username, value);
				UsernameError = null;
			}
		}

		private string _usernameError;
		public string UsernameError
		{
			get => _usernameError;
			set => SetProperty(ref _usernameError, value);
		}

		private string _password;
		public string Password
		{
			get => _password;
			set
			{
				SetProperty(ref _password, value);
				PasswordError = null;
			}
		}

		private string _passwordError;
		public string PasswordError
		{
			get => _passwordError;
			set => SetProperty(ref _passwordError, value);
		}

		public ICommand ConnectCommand { get; }
		public ICommand CloseCommand { get; }

		public LoginViewModel()
		{
			ConnectCommand = new Command(ConnectAction);
			CloseCommand = new Command(CloseAction);
		}

		private async void CloseAction()
		{
			await NavigationService.PopAsync();
		}

		private async void ConnectAction()
		{
			try
			{
				UsernameError = null;
				PasswordError = null;

				var connectService = DependencyService.Get<IConnectedUserService>();
				await connectService.Connect(Username, Password);
				await NavigationService.PopAsync();
			}
			catch (EmptyFieldException userEx) when (userEx.FieldName == "username")
			{
				UsernameError = "Veuillez remplir votre username";
			}
			catch (EmptyFieldException passEx) when (passEx.FieldName == "password")
			{
				PasswordError = "Veuillez remplir votre mot de passe";
			}
		}
	}
}
