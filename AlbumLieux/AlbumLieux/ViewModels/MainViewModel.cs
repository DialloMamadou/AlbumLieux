using AlbumLieux.Models;
using AlbumLieux.Services;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly Lazy<IPlacesDataServices> _placesService = new Lazy<IPlacesDataServices>(() => DependencyService.Get<IPlacesDataServices>());

		public ICommand ItemSelectedCommand { get; }

		private List<Places> _spotList;

		public List<Places> SpotList
		{
			get => _spotList;
			set => SetProperty(ref _spotList, value);
		}

		public MainViewModel()
		{
			ItemSelectedCommand = new Command(OnItemClicked);
		}

		public override async Task OnResume()
		{
			await base.OnResume();
			if (SpotList == null)
			{
				SpotList = await _placesService.Value.ListPlaces();
			}
		}

		private async void OnItemClicked(object obj)
		{
			if (obj is Places selectedSpot)
			{
				await NavigationService.PushAsync<DetailPage>(new Dictionary<string, object>
				{
					["id"] = selectedSpot.Id
				});
			}
		}
	}
}
