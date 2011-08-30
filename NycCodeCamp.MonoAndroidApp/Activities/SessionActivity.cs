using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Session Details")]
    public class SessionActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Session);

            string sessionKey = Intent.GetStringExtra("key");
            Log.Debug("SessionActivity", sessionKey);
            var session = CodeCampApplication.CodeCampService.Repository.GetSession(sessionKey);

            Log.Debug("SessionActivity", (session == null).ToString());

            FindViewById<TextView>(Resource.Id.Title).Text = session.Title;
            FindViewById<TextView>(Resource.Id.Time).Text = string.Format("{0} - {1}",
                                                                          session.Starts.ToLocalTime().ToShortTimeString(),
                                                                          session.Ends.ToLocalTime().ToShortTimeString());
            FindViewById<TextView>(Resource.Id.Abstract).Text = session.Abstract;
            var room = FindViewById<TextView>(Resource.Id.Room);
            room.Text = session.Room;
            room.Click += delegate
                              {
                                  StartActivity(typeof(RoomsActivity));
                              };

            var speakerName = FindViewById<TextView>(Resource.Id.SpeakerName);
            speakerName.Text = session.Speaker.Name;
            speakerName.Click += delegate
                                     {
                                         var intent = new Intent();
                                         intent.SetClass(this, typeof(SpeakerActivity));
                                         intent.PutExtra("email", session.Speaker.Email);

                                         StartActivity(intent);
                                     };
        }
    }
}