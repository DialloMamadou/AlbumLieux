using Storm.Mvvm.Forms;
using AlbumLieux.ViewModels;

namespace AlbumLieux.Pages
{
	public partial class AddPlacePage : BaseContentPage
	{
		public AddPlacePage()
		{
			InitializeComponent();
			BindingContext = new AddPlaceViewModel();
		}
	}
}
