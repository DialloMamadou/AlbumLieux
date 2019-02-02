using AlbumLieux.ViewModels;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlbumLieux
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
		public DetailPage()
		{
			InitializeComponent();
			BindingContext = new DetailViewModel();
		}

		private void ShowMap(object sender, System.EventArgs e)
		{
			if (BindingContext is DetailViewModel detailVm)
			{
				detailVm.ShowLocationCommand.Execute(null);
			}
		}

		private void PublishComment(object sender, System.EventArgs e)
		{
			if (BindingContext is DetailViewModel detailVm)
			{
				detailVm.PublishNewCommentCommand.Execute(Input.Text);
			}
		}

		private void Disconnect(object sender, System.EventArgs e)
		{
			if (BindingContext is DetailViewModel detailVm)
			{
				detailVm.DisconnectCommand.Execute(null);
			}
		}

		private void Connect(object sender, System.EventArgs e)
		{
			if (BindingContext is DetailViewModel detailVm)
			{
				detailVm.ConnectCommand.Execute(null);
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if(BindingContext is IViewModelLifecycle viewModelLifecycle)
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