using System;
using System.Windows.Input;
using AlbumLieux.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace AlbumLieux.ViewModels
{
    public class UpdatePasswordViewModel : ViewModelBase
    {
        private readonly Lazy<IProfileDataService> _profileService;

        #region Properties

        private string _oldPassword;

        public string OldPassword
        {
            get => _oldPassword;
            set => SetProperty(ref _oldPassword, value);
        }

        private string _oldPasswordError;

        public string OldPasswordError
        {
            get => _oldPasswordError;
            set => SetProperty(ref _oldPasswordError, value);
        }

        private string _newPassword;

        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        private string _newPasswordError;

        public string NewPasswordError
        {
            get => _newPasswordError;
            set => SetProperty(ref _newPasswordError, value);
        }


        private string _confirmPassword;

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private string _confirmPasswordError;

        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set => SetProperty(ref _confirmPasswordError, value);
        }

        #endregion

        public ICommand UpdatePasswordCommand { get; }

        public UpdatePasswordViewModel()
        {
            _profileService = new Lazy<IProfileDataService>(() => DependencyService.Resolve<IProfileDataService>());

            UpdatePasswordCommand = new Command(UpdatePasswordAction);
        }

        private async void UpdatePasswordAction()
        {
            OldPasswordError = null;
            NewPasswordError = null;
            ConfirmPasswordError = null;
            bool error = false;
            bool confirm = true;
            if (string.IsNullOrEmpty(OldPassword))
            {
                OldPasswordError = "Veuillez remplir votre ancien mot de passe";
                error = true;
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                NewPasswordError = "Veuillez remplir votre nouveau mot de passe";
                confirm = false;
                error = true;

            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                ConfirmPasswordError = "Veuillez confirmer votre mot de passe";
                confirm = false;
                error = true;
            }

            if (confirm && NewPassword != ConfirmPassword)
            {
                ConfirmPasswordError = "Votre nouveau mot de passe et sa confirmation sont différents !";
                error = true;
            }

            if (!error)
            {
                await _profileService.Value.UpdatePassword(NewPassword, OldPassword);
                await NavigationService.PopAsync();
            }
        }
    }
}
