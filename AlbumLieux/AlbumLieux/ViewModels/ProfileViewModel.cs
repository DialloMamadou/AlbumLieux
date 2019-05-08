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
	public class ProfileViewModel : ViewModelBase
	{
		private readonly Lazy<IProfileDataService> _profileDataService = new Lazy<IProfileDataService>(() => DependencyService.Resolve<IProfileDataService>());
		private readonly Lazy<ITokenService> _tokenService = new Lazy<ITokenService>(() => DependencyService.Resolve<ITokenService>());
		private readonly Lazy<IDialogService> _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

		private string _imageUrl;

		public string ImageUrl
		{
			get => _imageUrl;
			set => SetProperty(ref _imageUrl, value);
		}

		private string _firstName;

		public string FirstName
		{
			get => _firstName;
			set => SetProperty(ref _firstName, value);
		}

		private string _lastName;

		public string LastName
		{
			get => _lastName;
			set => SetProperty(ref _lastName, value);
		}

		private string _email;

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public ICommand UpdateMeCommand { get; }
		public ICommand UpdatePasswordCommand { get; }
		public ICommand DisconnectCommand { get; }

		public ProfileViewModel()
		{
			UpdateMeCommand = new Command(UpdateProfileAction);
			UpdatePasswordCommand = new Command(UpdatePasswordAction);
			DisconnectCommand = new Command(DisconnectAction);
		}

		public override async Task OnResume()
		{
			await base.OnResume();
			try
			{
				var user = await _profileDataService.Value.GetMe();
				ImageUrl = user.ImageUrl;
				FirstName = user.FirstName;
				LastName = user.LastName;
				Email = user.Email;
			}
			catch (ApiException apiEx)
			{
				await _dialogService.Value.ShowAlertDialog("Erreur", apiEx.ErrorMessage, "Ok");
			}
		}

		private async void DisconnectAction()
		{
			_tokenService.Value.Disconnect();
			await NavigationService.PopAsync();
		}

		public async void UpdateProfileAction()
		{
			await NavigationService.PushAsync<UpdateProfilePage>();
		}

		public async void UpdatePasswordAction()
		{
			await NavigationService.PushAsync<UpdatePasswordPage>();
		}
	}
}
