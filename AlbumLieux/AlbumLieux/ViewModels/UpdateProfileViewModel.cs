using AlbumLieux.Services;
using Storm.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Media;

namespace AlbumLieux.ViewModels
{

	public class UpdateProfileViewModel : BaseMediaViewModel
	{
		private readonly Lazy<IProfileDataService> _profileService;

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
		public ICommand UpdateImageFromPhotoCommand { get; }

		public UpdateProfileViewModel()
		{
			_profileService = new Lazy<IProfileDataService>(() => DependencyService.Resolve<IProfileDataService>());

			UpdateCommand = new Command(UpdateProfileAction);
			UpdateImageFromGalleryCommand = new Command(UpdateImageFromGalleryAction);
			UpdateImageFromPhotoCommand = new Command(UpdateImageFromPhotoAction);
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

		public async void UpdateProfileAction()
		{
			await _profileService.Value.UpdateMe(FirstName, LastName, ImageId ?? 0);
			await NavigationService.PopAsync();
		}

		public async void UpdateImageFromGalleryAction()
		{
			var mediafile = await PickFromGallery();
		}

		public async void UpdateImageFromPhotoAction()
		{
			var mediafile = await PickFromPhoto();
		}
	}
}
