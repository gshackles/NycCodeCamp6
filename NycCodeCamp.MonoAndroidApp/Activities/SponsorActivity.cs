using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Sponsor Details")]
    public class SponsorActivity : Activity
    {
        private string _website;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Sponsor);
            
            _website = Intent.GetStringExtra("website");

            FindViewById<TextView>(Resource.Id.SponsorName).Text = Intent.GetStringExtra("name");
            FindViewById<TextView>(Resource.Id.Description).Text = Intent.GetStringExtra("description");
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);

            new MenuInflater(this).Inflate(Resource.Menu.SponsorOptionsMenu, menu);

            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            menu.FindItem(Resource.Id.Web).SetVisible(!string.IsNullOrEmpty(_website));

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Web:
                    StartActivity(
                        new Intent(Intent.ActionView, 
                                   Android.Net.Uri.Parse(_website)));
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }

            return true;
        }
    }
}