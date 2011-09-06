using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Speaker Details")]
    public class SpeakerActivity : CampActivityBase
    {
        private Speaker _speaker;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Speaker);

            string email = Intent.GetStringExtra("email");
            _speaker = CodeCampApplication.CodeCampService.Repository.GetSpeaker(email);

            FindViewById<TextView>(Resource.Id.SpeakerName).Text = _speaker.Name;
            FindViewById<TextView>(Resource.Id.Bio).Text = _speaker.Bio;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            new MenuInflater(this).Inflate(Resource.Menu.SpeakerOptionsMenu, menu);

            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            menu.FindItem(Resource.Id.Web).SetVisible(!string.IsNullOrEmpty(_speaker.Website));

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Email:
                    emailSpeaker();
                    break;
                case Resource.Id.Web:
                    StartActivity(
                        new Intent(Intent.ActionView, 
                                   Android.Net.Uri.Parse(_speaker.Website)));
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }

            return true;
        }

        private void emailSpeaker()
        {
            var emailIntent = new Intent(Intent.ActionSend);

            emailIntent.PutExtra(Intent.ExtraEmail, new[] { _speaker.Email });
            emailIntent.SetType("plain/text");

            StartActivity(Intent.CreateChooser(emailIntent, "Send using:"));  
        }
    }
}