using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using FFImageLoading;

namespace AlbumLieux.Droid.Controls
{
	public class MainListAdapter : BaseListAdapter<Spot>
	{
		public MainListAdapter(Context context) : base(context) { }

		public MainListAdapter(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

		protected override int LayoutResId => Resource.Layout.list_item_main;

		protected override void HandleData(int position, Spot data, View root)
		{
			var name=root.FindViewById<TextView>(Resource.Id.Name);
			var city=root.FindViewById<TextView>(Resource.Id.City);
			var image=root.FindViewById<ImageViewAsync>(Resource.Id.Image);

			name.Text = data.Name;
			city.Text = data.City;
			ImageService.Instance.LoadUrl(data.ImageUrl)
				.LoadingPlaceholder("loading", FFImageLoading.Work.ImageSource.CompiledResource)
				.Into(image);
		}
	}
}
