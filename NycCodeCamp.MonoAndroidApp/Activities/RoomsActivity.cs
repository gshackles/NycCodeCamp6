using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using CodeCamp.Core.Entities;
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

            var rooms = CodeCampApplication.CodeCampService.Repository.GetRooms();
            var roomList = FindViewById<ListView>(Resource.Id.Rooms);
            roomList.Adapter = new RoomListAdapter(this, rooms);
            roomList.ItemClick += (sender, e) => viewRoom(rooms[e.Position]);
        }

        private void viewRoom(Room room)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof (RoomActivity));
            intent.PutExtra("name", room.Name);
            intent.PutExtra("key", room.Key);

            StartActivity(intent);
        }
    }
}