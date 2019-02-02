using AlbumLieux.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumLieux.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			BindingContext = new LoginViewModel();
		}

		private void Connect(object sender, System.EventArgs e)
		{
			if (BindingContext is LoginViewModel loginVm)
			{
				loginVm.ConnectCommand.Execute(null);
			}
		}
	}
}