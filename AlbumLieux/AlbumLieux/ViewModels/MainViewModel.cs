using Newtonsoft.Json;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private const string _data = @"
[
	{
		'name': 'Yasaka Shrine',
		'city': 'Kyoto',
		'image': 'https://source.unsplash.com/Xq1ntWruZQI',
		'latitude': 35.0036559,
		'longitude': 135.7763647
	},
	{
		'name': 'Fushimi Inari Shrine',
		'city': 'Kyoto',
		'image': 'https://source.unsplash.com/NYyCqdBOKwc',
		'latitude': 34.9671402,
		'longitude': 135.770483
	},
	{
		'name': 'Bamboo Forest',
		'city': 'Kyoto',
		'image': 'https://source.unsplash.com/buF62ewDLcQ',
		'latitude': 34.9824139,
		'longitude': 135.6633093
	},
	{
		'name': 'Brooklyn Bridge',
		'city': 'New York',
		'image': 'https://source.unsplash.com/THozNzxEP3g',
		'latitude': 40.7058094,
		'longitude': -73.9981622
	},
	{
		'name': 'Empire State Building',
		'city': 'New York',
		'image': 'https://source.unsplash.com/USrZRcRS2Lw',
		'latitude': 40.7485452,
		'longitude': -73.9879522
	},
	{
		'name': 'The statue of Liberty',
		'city': 'New York',
		'image': 'https://source.unsplash.com/PeFk7fzxTdk',
		'latitude': 40.6892494,
		'longitude': -74.0466891
	},
	{
		'name': 'Louvre Museum',
		'city': 'Paris',
		'image': 'https://source.unsplash.com/LrMWHKqilUw',
		'latitude':	48.8606111,
		'longitude': 2.3354553
	},
	{
		'name': 'Eiffel Tower',
		'city': 'Paris',
		'image': 'https://source.unsplash.com/HN-5Z6AmxrM',
		'latitude': 48.8583701,
		'longitude': 2.2922926
	},
	{
		'name': 'Big Ben',
		'city': 'London',
		'image': 'https://source.unsplash.com/CdVAUADdqEc',
		'latitude': 51.5007292,
		'longitude': -0.1268141
	},
	{
		'name': 'St. Peter's Basilica',
		'city': 'Rome',
		'image': 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/77/0_Basilique_Saint-Pierre_-_Rome_%282%29.JPG/1920px-0_Basilique_Saint-Pierre_-_Rome_%282%29.JPG'
		'latitude': '41.8986',
		'longitude': '12.4533'
	},
	{
		'name': 'Acropolis',
		'city': 'Athens'
		'image': 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/The_Acropolis_of_Athens_viewed_from_the_Hill_of_the_Muses_%2814220794964%29.jpg/1280px-The_Acropolis_of_Athens_viewed_from_the_Hill_of_the_Muses_%2814220794964%29.jpg'
		'latitude': '37.970833',
		'longitude': '23.726111'
	},
	{
		'name': ' Giza pyramid complex'
		'city': 'Giza',
		'image': 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/96/Pyramids_of_the_Giza_Necropolis.jpg/1280px-Pyramids_of_the_Giza_Necropolis.jpg'
		'latitude': '29.976111',
		'longitude': '31.132778'
	}
]
";

		public ICommand ItemSelectedCommand { get; }

		private List<Spot> _spotList;

		public List<Spot> SpotList
		{
			get => _spotList;
			set => SetProperty(ref _spotList, value);
		}

		public MainViewModel()
		{
			SpotList = JsonConvert.DeserializeObject<List<Spot>>(_data.Replace('\'', '"'));
			ItemSelectedCommand = new Command(OnItemClicked);
		}

		private async void OnItemClicked(object obj)
		{
			if (obj is Spot selectedSpot)
			{
				await NavigationService.PushAsync<DetailPage>(new Dictionary<string, object>
				{
					["name"] = selectedSpot.Name,
					["city"] = selectedSpot.City,
					["imageUrl"] = selectedSpot.ImageUrl,
					["latitude"] = selectedSpot.Latitude,
					["longitude"] = selectedSpot.Longitude
				});
			}
		}
	}
}
