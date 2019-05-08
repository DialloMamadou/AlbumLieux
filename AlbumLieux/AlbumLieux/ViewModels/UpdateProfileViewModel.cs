using AlbumLieux.Exceptions;
using AlbumLieux.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
	public class UpdateProfileViewModel : BaseMediaViewModel
	{
		private readonly Lazy<IProfileDataService> _profileService = new Lazy<IProfileDataService>(() => DependencyService.Resolve<IProfileDataService>());
		private readonly Lazy<IImageService> _imageService = new Lazy<IImageService>(() => DependencyService.Resolve<IImageService>());
		private readonly Lazy<IDialogService> _dialogService = new Lazy<IDialogService>(() => DependencyService.Resolve<IDialogService>());

		#region Properties

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

		public int? ImageId { get; set; }

		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set => SetProperty(ref _imageUrl, value);
		}

		#endregion

		public ICommand UpdateCommand { get; }
		public ICommand UpdateImageFromGalleryCommand { get; }
		public ICommand UpdateImageFromCameraCommand { get; }

		public UpdateProfileViewModel()
		{
			UpdateCommand = new Command(UpdateProfileAction);
			UpdateImageFromGalleryCommand = new Command(UpdateImageFromGalleryAction);
			UpdateImageFromCameraCommand = new Command(UpdateImageFromCameraAction);
		}

		public override async Task OnResume()
		{
			await base.OnResume();
			if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(ImageUrl))
			{
				var user = await _profileService.Value.GetMe();
				if (string.IsNullOrEmpty(FirstName))
				{
					FirstName = user.FirstName;
				}

				if (string.IsNullOrEmpty(LastName))
				{
					LastName = user.LastName;
				}

				if (string.IsNullOrEmpty(ImageUrl))
				{
					ImageId = user.ImageId;
					ImageUrl = user.ImageUrl;
				}
			}
		}

		private async void UpdateProfileAction()
		{
			try
			{
				await _profileService.Value.UpdateMe(FirstName, LastName, ImageId ?? 0);
				await NavigationService.PopAsync();
			}
			catch (ApiException apiEx)
			{
				await _dialogService.Value.ShowAlertDialog("Erreur", apiEx.ErrorMessage, "Ok");
			}
		}

		private async void UpdateImageFromGalleryAction()
		{
			try
			{
				var mediafile = await PickFromGallery();
				if (mediafile != null)
				{
					var image = await _imageService.Value.UploadImage(mediafile.GetStream());
					if (image != null)
					{
						ImageId = image.Id;
						ImageUrl = image.Url;
					}
				}
			}
			catch (NotSupportedException)
			{
				await _dialogService.Value.ShowAlertDialog("Impossible", "L'action demandé est impossible à réaliser sur le device", "Ok");
			}
			catch (MissingPermissionException permEx)
			{
				await _dialogService.Value.ShowAlertDialog("Permission manquante", $"La permission {permEx.PermissionName} est manquante pour executer l'action demandée", "Ok");
			}
			catch (ApiException apiEx)
			{
				await _dialogService.Value.ShowAlertDialog("Erreur", apiEx.ErrorMessage, "Ok");
			}
		}

		private async void UpdateImageFromCameraAction()
		{
			try
			{
				var mediafile = await PickFromCamera();
				if (mediafile != null)
				{
					var image = await _imageService.Value.UploadImage(mediafile.GetStream());
					if (image != null)
					{
						ImageId = image.Id;
						ImageUrl = image.Url;
					}
				}
			}
			catch (NotSupportedException)
			{
				await _dialogService.Value.ShowAlertDialog("Impossible", "L'action demandé est impossible à réaliser sur le device", "Ok");
			}
			catch (MissingPermissionException permEx)
			{
				await _dialogService.Value.ShowAlertDialog("Permission manquante", $"La permission {permEx.PermissionName} est manquante pour executer l'action demandée", "Ok");
			}
			catch (ApiException apiEx)
			{
				await _dialogService.Value.ShowAlertDialog("Erreur", apiEx.ErrorMessage, "Ok");
			}
		}
	}
}
