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
            DependencyService.Register<IProfileDataService, ProfileDataService>();

            Barrel.ApplicationId = "ALBULIEUX_EXEMPLE";
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
