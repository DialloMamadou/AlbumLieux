using AlbumLieux.Exceptions;
using AlbumLieux.Pages;
using AlbumLieux.Services;
using Storm.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly Lazy<IUserDataService> _userService = new Lazy<IUserDataService>(() => DependencyService.Resolve<IUserDataService>());
		private readonly Lazy<IDialogService> _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

		#region Property

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

		#endregion

		public ICommand ConnectCommand { get; }
		public ICommand RegisterCommand { get; }

		public LoginViewModel()
		{
			ConnectCommand = new Command(ConnectAction);
			RegisterCommand = new Command(RegisterAction);
		}

		private async void RegisterAction()
		{
			await NavigationService.PushAsync<RegisterPage>();
		}

		private async void ConnectAction()
		{
			bool error = false;
			UsernameError = null;
			PasswordError = null;
			if (string.IsNullOrEmpty(Username))
			{
				UsernameError = "Veuillez remplir votre username";
				error = true;
			}

			if (string.IsNullOrEmpty(Password))
			{
				PasswordError = "Veuillez remplir votre mot de passe";
				error = false;
			}

			if (!error)
			{
				try
				{
					await _userService.Value.Connect(Username, Password);
					await NavigationService.PopAsync();
					await NavigationService.PushAsync<ProfilePage>();
				}
				catch (ApiException apiEx)
				{
					await _dialogService.Value.ShowAlertDialog("Erreur", apiEx.ErrorMessage, "Ok");
				}
			}
		}
	}
}
