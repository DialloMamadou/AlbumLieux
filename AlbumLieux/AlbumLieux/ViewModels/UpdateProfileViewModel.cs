using AlbumLieux.Services;
using Storm.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
    public class UpdateProfileViewModel : ViewModelBase
    {
        private Lazy<IProfileDataService> _profileService;

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

        #endregion

        public ICommand UpdateProfileCommand { get; }

        public UpdateProfileViewModel()
        {
            _profileService = new Lazy<IProfileDataService>(() => DependencyService.Resolve<IProfileDataService>());

            UpdateProfileCommand = new Command(UpdateProfileAction);
        }

        public override async Task OnResume()
        {
            await base.OnResume();
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
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
            }
        }

        public async void UpdateProfileAction()
        {
            //TODO
        }
    }
}
