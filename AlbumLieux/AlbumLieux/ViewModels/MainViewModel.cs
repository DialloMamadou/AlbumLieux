using AlbumLieux.Models;
using AlbumLieux.Pages;
using AlbumLieux.Services;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;
using System.Linq;

namespace AlbumLieux.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly Lazy<IPlacesDataServices> _placesService = new Lazy<IPlacesDataServices>(() => DependencyService.Get<IPlacesDataServices>());
		private readonly Lazy<ITokenService> _tokenService = new Lazy<ITokenService>(() => DependencyService.Get<ITokenService>());
		private readonly Lazy<IGeolocationService> _geoService = new Lazy<IGeolocationService>(() => DependencyService.Get<IGeolocationService>());

		public ICommand RefreshCommand { get; }
		public ICommand ProfileCommand { get; }

		private bool _isBusy;

		public bool IsBusy
		{
			get => _isBusy;
			set => SetProperty(ref _isBusy, value);
		}

		private Places _selectedPlace;

		public Places SelectedPlace
		{
			get => _selectedPlace;
			set
			{
				if (SetProperty(ref _selectedPlace, value) && value != null)
				{
					OnItemClicked(value);
				}
			}
		}

		private List<Places> _spotList;

		public List<Places> SpotList
		{
			get => _spotList;
			set => SetProperty(ref _spotList, value);
		}

		public MainViewModel()
		{
			RefreshCommand = new Command(RefreshAction);
			ProfileCommand = new Command(ProfileAction);
		}

		public override async Task OnResume()
		{
			try
			{
				await base.OnResume();

				IsBusy = true;

				await RefreshList();

				SelectedPlace = null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async void ProfileAction()
		{
			if (_tokenService.Value.HasToken())
			{
				await NavigationService.PushAsync<ProfilePage>();
			}
			else
			{
				await NavigationService.PushAsync<LoginPage>();
			}
		}

		private async Task RefreshList(bool force = false)
		{
			var list = await _placesService.Value.ListPlaces(true);
			var position = await _geoService.Value.GetMyPosition();

			SpotList = list.Select(x =>
			{
				x.DistanceToMe = position.CalculateDistance(new Position(x.Latitude, x.Longitude), GeolocatorUtils.DistanceUnits.Kilometers);
				return x;
			}).OrderBy(x => x.DistanceToMe).ToList();
		}

		private async void RefreshAction()
		{
			IsBusy = true;
			await RefreshList(true);
			IsBusy = false;
		}

		private async Task OnItemClicked(Places obj)
		{
			await NavigationService.PushAsync<DetailTabbedPage>(new Dictionary<string, object>
			{
				["id"] = obj.Id
			});
		}
	}
}