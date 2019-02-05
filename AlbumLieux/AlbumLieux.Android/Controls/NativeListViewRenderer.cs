using System.ComponentModel;
using AlbumLieux.Controls;
using AlbumLieux.Droid.Controls;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NativeListView), typeof(NativeListViewRenderer))]
namespace AlbumLieux.Droid.Controls
{
	public class NativeListViewRenderer : ListViewRenderer
	{
		private MainListAdapter _adapter;

		public NativeListViewRenderer(Context context) : base(context) { }

		protected override Android.Widget.ListView CreateNativeControl()
		{
			var res = base.CreateNativeControl();
			_adapter = new MainListAdapter(Context);
			if(Element is NativeListView listView)
			{
				_adapter.ItemList = listView.Items;
				res.Adapter = _adapter;
				res.Invalidate();
			}
			return res;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
		}
	}
}
