using AlbumLieux.Models;
using AlbumLieux.Pages;
using AlbumLieux.Services;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;

namespace AlbumLieux.ViewModels
{
	public class DetailViewModel : ViewModelBase
	{
		private readonly Lazy<IConnectedUserService> _connectedUserService = new Lazy<IConnectedUserService>(() => DependencyService.Get<IConnectedUserService>());

		private string _connectedUserName { get; set; }

		#region Properties

		private string _city;
		[NavigationParameter("city")]
		public string City
		{
			get => _city;
			set => SetProperty(ref _city, value);
		}

		private double _latitude;
		[NavigationParameter("latitude")]
		public double Latitude
		{
			get => _latitude;
			set => SetProperty(ref _latitude, value);
		}

		private double _longitude;
		[NavigationParameter("longitude")]
		public double Longitude
		{
			get => _longitude;
			set => SetProperty(ref _longitude, value);
		}


		private string _name;
		[NavigationParameter("name")]
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		private string _imageUrl;
		[NavigationParameter("imageUrl")]
		public string ImageUrl
		{
			get => _imageUrl;
			set => SetProperty(ref _imageUrl, value);
		}

		private bool _isConnected;

		public bool IsConnected
		{
			get => _isConnected;
			set => SetProperty(ref _isConnected, value);
		}

		private string _comment;

		public string Comment
		{
			get => _comment;
			set => SetProperty(ref _comment, value);
		}

		public ObservableCollection<Comment> CommentList { get; }

		#endregion

		public ICommand ConnectCommand { get; }
		public ICommand DisconnectCommand { get; }
		public ICommand PublishNewCommentCommand { get; }
		public ICommand ShowLocationCommand { get; }

		public DetailViewModel()
		{
			ShowLocationCommand = new Command(ShowLocationAction);
			ConnectCommand = new Command(ConnectAction);
			DisconnectCommand = new Command(DisconnectAction);
			PublishNewCommentCommand = new Command(PublishNewCommentAction);

			CommentList = new ObservableCollection<Comment>();
			OnPropertyChanged(nameof(CommentList));
		}

		private void PublishNewCommentAction()
		{
			CommentList.Add(new Comment
			{
				Content = Comment,
				PublishDate = DateTime.Now,
				PublisherName = _connectedUserName
			});

			Comment = string.Empty;
		}

		public override async Task OnResume()
		{
			await base.OnResume();
			IsConnected = _connectedUserService.Value.IsConnected;
			_connectedUserName = _connectedUserService.Value.CurrentUserName;
		}

		public override Task OnPause()
		{
			return base.OnPause();
		}

		private async void DisconnectAction(object obj)
		{
			await _connectedUserService.Value.Disconnect();
			IsConnected = _connectedUserService.Value.IsConnected;
			_connectedUserName = _connectedUserService.Value.CurrentUserName;
		}

		private async void ConnectAction(object obj)
		{
			await NavigationService.PushAsync<LoginPage>(mode: Storm.Mvvm.Services.NavigationMode.Modal);
		}

		private async void ShowLocationAction(object _)
		{
			var mapService = DependencyService.Get<IMapService>();

			await mapService.ShowMap(Latitude, Longitude, Name);
		}
	}
}
