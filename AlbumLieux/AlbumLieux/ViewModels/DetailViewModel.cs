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

namespace AlbumLieux.ViewModels
{
	public class DetailViewModel : ViewModelBase
	{
		private readonly Lazy<ITokenService> _tokenService = new Lazy<ITokenService>(() => DependencyService.Get<ITokenService>());
		private readonly Lazy<IPlacesDataServices> _placesDataServices = new Lazy<IPlacesDataServices>(() => DependencyService.Get<IPlacesDataServices>());

		private string _connectedUserName { get; set; }

		#region Properties

		private uint _id;
		[NavigationParameter("id")]
		public uint Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		private double _latitude;
		public double Latitude
		{
			get => _latitude;
			set => SetProperty(ref _latitude, value);
		}

		private double _longitude;
		public double Longitude
		{
			get => _longitude;
			set => SetProperty(ref _longitude, value);
		}

		private string _name;
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		private string _imageUrl;
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
		}

		private void PublishNewCommentAction()
		{
			CommentList.Add(new Comment
			{
				Content = Comment,
				Date = DateTime.Now,
				Author = _connectedUserName
			});

			Comment = string.Empty;
		}

		public override async Task OnResume()
		{
			await base.OnResume();
			var placeTask=_placesDataServices.Value.GetPlace(Id);

			//IsConnected = _tokenService.Value.IsConnected;
			//_connectedUserName = _tokenService.Value.CurrentUserName;

			var place= await placeTask;
			Name = place.Name;
			Description = place.Description;
			Latitude = place.Latitude;
			Longitude = place.Longitude;
			CommentList.Clear();
			place.CommentList?.ForEach(x => CommentList.Add(x));
			ImageUrl = place.ImageUrl;
		}

		private async void DisconnectAction(object obj)
		{
			//await _tokenService.Value.Disconnect();
			//IsConnected = _tokenService.Value.IsConnected;
			//_connectedUserName = _tokenService.Value.CurrentUserName;
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
