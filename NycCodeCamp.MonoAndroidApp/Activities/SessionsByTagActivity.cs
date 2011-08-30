using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using NycCodeCamp.MonoAndroidApp.ListAdapters;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Sessions By Tag")]
    public class SessionsByTagActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SessionsByTag);

            string tag = Intent.GetStringExtra("tag");
            var sessions = CodeCampApplication.CodeCampService.Repository.GetSessionsByTag(tag);

            Title = tag;

            var sessionList = FindViewById<ListView>(Resource.Id.Sessions);
            sessionList.Adapter = new SessionsByTagListAdapter(this, sessions);
            sessionList.ItemClick += (sender, e) =>
                                         {
                                             var intent = new Intent();
                                             intent.SetClass(this, typeof (SessionActivity));
                                             intent.PutExtra("key", sessions[e.Position].Key);

                                             StartActivity(intent);
                                         };
        }
    }
}