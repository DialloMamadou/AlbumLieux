using AlbumLieux.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumLieux
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : BaseContentPage
	{
		public DetailPage()
		{
			InitializeComponent();
			BindingContext = new DetailViewModel();
		}
	}
}