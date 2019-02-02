using AlbumLieux.ViewModels;
using Xamarin.Forms;

namespace AlbumLieux
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new MainViewModel();
		}

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (BindingContext is MainViewModel mainVm)
			{
				mainVm.ItemSelectedCommand.Execute(e.Item);
			}
		}
	}
}
