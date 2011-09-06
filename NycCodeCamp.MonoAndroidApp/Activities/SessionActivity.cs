using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using NycCodeCamp.MonoAndroidApp.Extensions;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Session Details")]
    public class SessionActivity : CampActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Session);

            string sessionKey = Intent.GetStringExtra("key");
            var session = CodeCampApplication.CodeCampService.Repository.GetSession(sessionKey);

            FindViewById<TextView>(Resource.Id.Title).Text = session.Title;
            FindViewById<TextView>(Resource.Id.Time).Text = string.Format("{0} - {1}",
                                                                          session.Starts.ToLocalTime().ToShortTimeString(),
                                                                          session.Ends.ToLocalTime().ToShortTimeString());
            FindViewById<TextView>(Resource.Id.Abstract).Text = session.Abstract;
            var room = FindViewById<TextView>(Resource.Id.Room);
            room.Text = "Room: " + session.Room;
            room.Click += delegate
                              {
                                  var intent = new Intent();
                                  intent.SetClass(this, typeof (RoomActivity));
                                  intent.PutExtra("key", session.RoomKey);

                                  StartActivity(intent);
                              };

            var speakerName = FindViewById<TextView>(Resource.Id.SpeakerName);
            speakerName.ShowIf(!string.IsNullOrEmpty(session.Speaker.Name));
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