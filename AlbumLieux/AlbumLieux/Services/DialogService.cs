using Storm.Mvvm.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlbumLieux.Services
{
	public interface IDialogService
	{
		Task ShowAlertDialog(string title, string message, string cancelButton);
	}

	public class DialogService : IDialogService
	{
		private readonly Lazy<ICurrentPageService> _currentPageService = new Lazy<ICurrentPageService>(() => DependencyService.Resolve<ICurrentPageService>());

		public Task ShowAlertDialog(string title, string message, string button)
		{
			return _currentPageService.Value.CurrentPage.DisplayAlert(title, message, button);
		}
	}
}
