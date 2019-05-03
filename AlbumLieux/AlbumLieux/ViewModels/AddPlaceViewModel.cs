using System.Windows.Input;
using System;
using AlbumLieux.Services;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
	public class AddPlaceViewModel : BaseMediaViewModel
	{
		private readonly Lazy<IGeolocationService> _geoService = new Lazy<IGeolocationService>(() => DependencyService.Resolve<IGeolocationService>());
		private readonly Lazy<IPlacesDataServices> _placeService = new Lazy<IPlacesDataServices>(() => DependencyService.Resolve<IPlacesDataServices>());
		private readonly Lazy<IImageService> _imageService = new Lazy<IImageService>(() => DependencyService.Resolve<IImageService>());

		#region Properties

		private string _title;
		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value);
		}

		private string _titleError;
		public string TitleError
		{
			get => _titleError;
			set => SetProperty(ref _titleError, value);
		}

		private string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		private double? _latitude;
		public double? Latitude
		{
			get => _latitude;
			set => SetProperty(ref _latitude, value);
		}

		private string _latitudeError;
		public string LatitudeError
		{
			get => _latitudeError;
			set => SetProperty(ref _latitudeError, value);
		}

		private double? _longitude;
		public double? Longitude
		{
			get => _longitude;
			set => SetProperty(ref _longitude, value);
		}

		private string _longitudeError;
		public string LongitudeError
		{
			get => _longitudeError;
			set => SetProperty(ref _longitudeError, value);
		}

		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set => SetProperty(ref _imageUrl, value);
		}

		public int ImageId { get; set; }

		#endregion

		public ICommand AddCommand { get; }
		public ICommand PickImageFromGalleryCommand { get; }
		public ICommand PickImageFromCameraCommand { get; }
		public ICommand TakeMyLocationCommand { get; }

		public AddPlaceViewModel()
		{
			PickImageFromGalleryCommand = new Command(UpdateImageFromGalleryAction);
			PickImageFromCameraCommand = new Command(UpdateImageFromCameraAction);
			TakeMyLocationCommand = new Command(TakeMyLocationAction);
			AddCommand = new Command(AddAction);
		}

		private async void AddAction()
		{
			bool error = false;
			if (string.IsNullOrEmpty(Title))
			{
				TitleError = "Veuillez indiquer un nom de lieu";
				error = true;
			}

			if (!Latitude.HasValue)
			{
				LatitudeError = "Veuillez indiquer une latitude";
				error = true;
			}

			if (!Longitude.HasValue)
			{
				LongitudeError = "Veuillez indidiquer une longitude";
				error = true;
			}

			if (!error)
			{
				await _placeService.Value.AddPlace(Title, Description, ImageId, Latitude.Value, Longitude.Value);
				await NavigationService.PopAsync();
			}
		}

		private async void TakeMyLocationAction()
		{
			var position = await _geoService.Value.GetMyPosition();
			Latitude = position.Latitude;
			Longitude = position.Longitude;
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

