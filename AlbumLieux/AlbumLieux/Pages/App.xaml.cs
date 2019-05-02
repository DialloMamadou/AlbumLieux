using AlbumLieux.Services;
using MonkeyCache.SQLite;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AlbumLieux
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new MainPage())
        {
            InitializeComponent();

            DependencyService.Register<IUserDataService, UserDataService>();
            DependencyService.Register<IPlacesDataServices, PlacesDataServices>();
            DependencyService.Register<ITokenService, TokenService>();
            DependencyService.Register<IGeolocationService, GeolocationService>();
            DependencyService.Register<IProfileDataService, ProfileDataService>();
            DependencyService.Register<IImageService, ImageService>();

            Barrel.ApplicationId = "ALBULIEUX_EXEMPLE";
            Barrel.Current.EmptyExpired();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
