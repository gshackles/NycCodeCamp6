using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Room")]
    public class RoomActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Room);

            Title = Intent.GetStringExtra("name");

            var mapImage = FindViewById<WebView>(Resource.Id.MapImage);

            mapImage.LoadUrl("file:///android_asset/" + Intent.GetStringExtra("filename"));
            mapImage.Settings.SetSupportZoom(true);
            mapImage.Settings.BuiltInZoomControls = true;
            mapImage.Settings.UseWideViewPort = true;
            mapImage.Settings.DefaultZoom = WebSettings.ZoomDensity.Far;
        }
    }
}