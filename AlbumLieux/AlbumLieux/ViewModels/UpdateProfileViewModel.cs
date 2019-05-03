using AlbumLieux.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
    public class UpdateProfileViewModel : BaseMediaViewModel
    {
        private readonly Lazy<IProfileDataService> _profileService;
        private readonly Lazy<IImageService> _imageService;

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
            _profileService = new Lazy<IProfileDataService>(() => DependencyService.Resolve<IProfileDataService>());
            _imageService = new Lazy<IImageService>(() => DependencyService.Resolve<IImageService>());

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
            await _profileService.Value.UpdateMe(FirstName, LastName, ImageId ?? 0);
            await NavigationService.PopAsync();
        }

		private async void UpdateImageFromGalleryAction()
        {
            var mediafile = await PickFromGallery();
            var image = await _imageService.Value.UploadImage(mediafile.GetStream());
            if (image != null)
            {
                ImageId = image.Id;
                ImageUrl = image.Url;
            }
        }

		private async void UpdateImageFromCameraAction()
        {
            var mediafile = await PickFromCamera();
            var image = await _imageService.Value.UploadImage(mediafile.GetStream());
            if (image != null)
            {
                ImageId = image.Id;
                ImageUrl = image.Url;
            }
        }
    }
}
