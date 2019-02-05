using System;
using Xamarin.Forms;
using System.Collections.Generic;
namespace AlbumLieux.Controls
{
	public class NativeListView : ListView
	{
		public static readonly BindableProperty ItemsProperty =
			  BindableProperty.Create("Items", typeof(List<Spot>), typeof(NativeListView), new List<Spot>());

		public List<Spot> Items
		{
			get { return (List<Spot>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}
	}
}
