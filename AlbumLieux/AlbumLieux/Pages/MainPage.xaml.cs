using AlbumLieux.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace AlbumLieux
{
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
