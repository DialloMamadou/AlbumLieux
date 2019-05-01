using AlbumLieux.ViewModels;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumLieux.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTabbedPage : TabbedPage
    {
        public DetailTabbedPage()
        {
            InitializeComponent();
            BindingContext = new DetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is IViewModelLifecycle viewModelLifecycle)
            {
                viewModelLifecycle.OnResume();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is IViewModelLifecycle viewModelLifecycle)
            {
                viewModelLifecycle.OnPause();
            }
        }
    }
}