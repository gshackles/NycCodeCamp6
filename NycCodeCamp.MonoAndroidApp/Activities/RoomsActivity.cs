using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Webkit;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Rooms")]
    public class RoomsActivity : CampActivityBase
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Rooms);

            var mapImage = FindViewById<WebView>(Resource.Id.MapImage);

            mapImage.LoadUrl("file:///android_asset/map.html");
            mapImage.Settings.SetSupportZoom(true);
            mapImage.Settings.BuiltInZoomControls = true;
            mapImage.Settings.UseWideViewPort = true;
            mapImage.Settings.DefaultZoom = WebSettings.ZoomDensity.Far;
        }
    }
}