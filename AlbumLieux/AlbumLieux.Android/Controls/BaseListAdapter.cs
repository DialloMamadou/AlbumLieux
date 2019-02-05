using System;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
namespace AlbumLieux.Droid.Controls
{
	public abstract class BaseListAdapter<TData> : BaseAdapter<TData>
	{
		private readonly Context _ctx;

		public BaseListAdapter(Context context)
		{
			_ctx = context;
		}

		public BaseListAdapter(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

		public List<TData> ItemList { get; set; }

		public override TData this[int position]
		{
			get
			{
				if (ItemList != null)
				{
					return ItemList[position];
				}
				else
				{
					return default(TData);
				}
			}
		}

		public override int Count => ItemList?.Count ?? 0;

		public override long GetItemId(int position) => position;

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var data = ItemList[position];
			var view = convertView;

			if (view == null)
			{
				view = LayoutInflater.From(_ctx).Inflate(LayoutResId, parent, false);
			}

			HandleData(position, data, view);

			return view;
		}

		protected abstract int LayoutResId { get; }
		protected abstract void HandleData(int position, TData data, View root);
	}
}
