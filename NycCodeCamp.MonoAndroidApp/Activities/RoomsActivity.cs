using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using NycCodeCamp.MonoAndroidApp.Entities;
using NycCodeCamp.MonoAndroidApp.ListAdapters;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Rooms")]
    public class RoomsActivity : CampActivityBase
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Rooms);

            var rooms = new List<Room>
			{
				new Room("Lower Level", "Auditorium, Cafeteria", "map.html"),
				new Room("Ground Level", "Student Union meeting spaces", "map.html"),
				new Room("Second Floor", "Classroom meeting spaces", "map.html")
			};

            var roomList = FindViewById<ListView>(Resource.Id.Rooms);
            roomList.Adapter = new RoomListAdapter(this, rooms);
            roomList.ItemClick += (sender, e) => viewRoom(rooms[e.Position]);
        }

        private void viewRoom(Room room)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof (RoomActivity));
            intent.PutExtra("name", room.Name);
            intent.PutExtra("filename", room.Filename);

            StartActivity(intent);
        }
    }
}