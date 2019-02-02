using AlbumLieux.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumLieux.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : BaseContentPage
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