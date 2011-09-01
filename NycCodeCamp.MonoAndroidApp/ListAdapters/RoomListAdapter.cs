using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using CodeCamp.Core.Entities;
using Object = Java.Lang.Object;

namespace NycCodeCamp.MonoAndroidApp.ListAdapters
{
    public class RoomListAdapter : BaseAdapter
    {
        private readonly Activity _context;
        private readonly IList<Room> _rooms;

        public RoomListAdapter(Activity context, IList<Room> rooms)
        {
            _context = context;
            _rooms = rooms;
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
            var view = (convertView
                        ?? _context.LayoutInflater.Inflate(Resource.Layout.RoomsItem, parent, false));
            var room = _rooms[position];

            view.FindViewById<TextView>(Resource.Id.Name).Text = room.Name;
            view.FindViewById<TextView>(Resource.Id.Description).Text = room.Description;

            return view;
        }

        public override int Count
        {
            get { return _rooms.Count; }
        }
    }
}