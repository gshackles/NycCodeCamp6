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
    public class SessionsByTagListAdapter : BaseAdapter
    {
        private readonly Activity _context;
        private readonly IList<Session> _sessions;

        public SessionsByTagListAdapter(Activity context, IList<Session> sessions)
        {
            _context = context;
            _sessions = sessions;
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
                        ?? _context.LayoutInflater.Inflate(Resource.Layout.SessionsByTagItem, parent, false));
            var session = _sessions[position];

            view.FindViewById<TextView>(Resource.Id.Title).Text = session.Title;
            view.FindViewById<TextView>(Resource.Id.Time).Text = string.Format("{0} - {1}",
                                                                               session.Starts.ToLocalTime().ToShortTimeString(),
                                                                               session.Ends.ToLocalTime().ToShortTimeString());
            view.FindViewById<TextView>(Resource.Id.Room).Text = session.Room;

            return view;
        }

        public override int Count
        {
            get { return _sessions.Count; }
        }
    }
}