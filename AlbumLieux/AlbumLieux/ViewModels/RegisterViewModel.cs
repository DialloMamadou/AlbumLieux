using System;
using System.Windows.Input;
using Storm.Mvvm;
using AlbumLieux.Pages;
using AlbumLieux.Services;
using Xamarin.Forms;
using AlbumLieux.Exceptions;

namespace AlbumLieux.ViewModels
{
	public class RegisterViewModel : ViewModelBase
	{
		private readonly Lazy<IUserDataService> _userService = new Lazy<IUserDataService>(() => DependencyService.Resolve<IUserDataService>());
		private readonly Lazy<IDialogService> _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

		#region Properties

		private string _email;
		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		private string _emailError;
		public string EmailError
		{
			get => _emailError;
			set => SetProperty(ref _emailError, value);
		}

		private string _password;
		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		private string _passwordError;
		public string PasswordError
		{
			get => _passwordError;
			set => SetProperty(ref _passwordError, value);
		}

		private string _firstName;
		public string FirstName
		{
			get => _firstName;
			set => SetProperty(ref _firstName, value);
		}

		private string _firstNameError;
		public string FirstNameError
		{
			get => _firstNameError;
			set => SetProperty(ref _firstNameError, value);
		}

		private string _lastName;
		public string LastName
		{
			get => _lastName;
			set => SetProperty(ref _lastName, value);
		}

		private string _lastNameError;
		public string LastNameError
		{
			get => _lastNameError;
			set => SetProperty(ref _lastNameError, value);
		}

		#endregion

		public ICommand RegisterCommand { get; }

		public RegisterViewModel()
		{
			RegisterCommand = new Command(RegisterAction);
		}

		private async void RegisterAction()
		{
			bool error = false;
			if (string.IsNullOrEmpty(Email))
			{
				EmailError = "Veuillez saisir votre email";
				error = true;
			}

			if (string.IsNullOrEmpty(Password))
			{
				PasswordError = "Veuillez saisir votre mot de passe";
				error = true;
			}

			if (string.IsNullOrEmpty(FirstName))
			{
				FirstNameError = "Veuillez saisir votre prénom";
				error = true;
			}

			if (string.IsNullOrEmpty(LastName))
			{
				LastNameError = "Veuillez saisir votre nom";
				error = true;
			}

			if (!error)
			{
				try
				{
					await _userService.Value.Register(Email, FirstName, LastName, Password);
					await NavigationService.PopAsync();
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
