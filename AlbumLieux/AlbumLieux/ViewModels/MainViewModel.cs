using AlbumLieux.Models;
using AlbumLieux.Pages;
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
        private readonly Lazy<ITokenService> _tokenService = new Lazy<ITokenService>(() => DependencyService.Get<ITokenService>());

        public ICommand ItemSelectedCommand { get; }
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
            await base.OnResume();

            if (SpotList == null)
            {
                IsBusy = true;
                SpotList = await _placesService.Value.ListPlaces();
                IsBusy = false;
            }
            SelectedPlace = null;

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

        private async void RefreshAction()
        {
            IsBusy = true;
            SpotList = await _placesService.Value.ListPlaces(true);
            IsBusy = false;
        }

        private async void OnItemClicked(Places obj)
        {
            await NavigationService.PushAsync<DetailPage>(new Dictionary<string, object>
            {
                ["id"] = obj.Id
            });
        }
    }
}
