using AlbumLieux.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumLieux.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdatePasswordPage : BaseContentPage
	{
		public UpdatePasswordPage()
		{
			InitializeComponent();
			BindingContext = new UpdatePasswordViewModel();
		}
	}
}