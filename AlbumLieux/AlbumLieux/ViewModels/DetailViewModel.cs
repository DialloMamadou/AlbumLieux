using AlbumLieux.Models;
using AlbumLieux.Services;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AlbumLieux.ViewModels
{
	public class DetailViewModel : ViewModelBase
	{
		private readonly Lazy<ITokenService> _tokenService = new Lazy<ITokenService>(() => DependencyService.Get<ITokenService>());
		private readonly Lazy<IPlacesDataServices> _placeService = new Lazy<IPlacesDataServices>(() => DependencyService.Get<IPlacesDataServices>());

		#region Properties

		[NavigationParameter("id")]
		public uint Id { get; set; }

		private string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

        private Position _position;

        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
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

        public ObservableCollection<Pin> MapPin { get; }

		#endregion

		public ICommand PublishNewCommentCommand { get; }

		public DetailViewModel()
		{
			PublishNewCommentCommand = new Command(PublishNewCommentAction);

			CommentList = new ObservableCollection<Comment>();
			MapPin = new ObservableCollection<Pin>();
        }

		public override async Task OnResume()
		{
			await base.OnResume();
			await ReloadPlace();
			IsConnected = _tokenService.Value.HasToken();
		}

		private async Task ReloadPlace()
		{
			var place = await _placeService.Value.GetPlace(Id);
			Name = place.Title;
			Description = place.Description;
            Position = new Position(place.Latitude, place.Longitude);
            MapPin.Clear();
            MapPin.Add(new Pin()
            {
                Position = Position,
                Label = place.Title
            });
			CommentList.Clear();
			place.CommentList?.ForEach(x => CommentList.Add(x));
			ImageUrl = place.ImageUrl;
		}

		private async void PublishNewCommentAction()
		{
			if (!string.IsNullOrEmpty(Comment))
			{
				await _placeService.Value.PostComment(Id, Comment);
				Comment = string.Empty;
				await ReloadPlace();
			}
		}
	}
}
