using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Webkit;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Room")]
    public class RoomActivity : CampActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Room);

            var room = CodeCampApplication.CodeCampService.Repository.GetRoom(Intent.GetStringExtra("key"));

            Title = room.Name;

            var mapImage = FindViewById<WebView>(Resource.Id.MapImage);

            mapImage.LoadUrl("file:///android_asset/Maps/" + room.Key + "/map.html");
            mapImage.Settings.SetSupportZoom(true);
            mapImage.Settings.BuiltInZoomControls = true;
            mapImage.Settings.UseWideViewPort = true;
            mapImage.Settings.DefaultZoom = WebSettings.ZoomDensity.Far;
        }
    }
}