using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace NycCodeCamp.MonoAndroidApp.ListAdapters
{
    public abstract class SectionListAdaperBase<TSection, TListItem> : BaseAdapter
    {
        private readonly Activity _context;
        private readonly IList<object> _items;
        private readonly int _itemResourceId;

        public SectionListAdaperBase(Activity context, IList<TSection> sections, Func<TSection, string> getHeaderText, Func<TSection, IEnumerable<TListItem>> getItems, int itemResourceId)
        {
            _context = context;
            _itemResourceId = itemResourceId;

            _items = new List<object>();

            foreach (var section in sections)
            {
                _items.Add(getHeaderText(section));

                foreach (var item in getItems(section))
                {
                    _items.Add(item);
                }
            }
        }

        public override Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            View view = null;

            // section heading
            if (item is string)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.SectionHeader, null);
                view.Clickable = false;
                view.LongClickable = false;
                view.SetOnClickListener(null);

                view.FindViewById<TextView>(Resource.Id.Title).Text = (string)item;
            }
            // item info
            else
            {
                view = _context.LayoutInflater.Inflate(_itemResourceId, null);
                PopulateItemView(view, (TListItem)item);
            }

            return view;
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public TListItem GetItemAtPosition(int position)
        {
            return (TListItem) _items[position];
        }

        protected abstract void PopulateItemView(View view, TListItem item);
    }
}