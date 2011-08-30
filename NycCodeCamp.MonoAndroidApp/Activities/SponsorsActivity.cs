using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using CodeCamp.Core.ViewModels;
using NycCodeCamp.MonoAndroidApp.ListAdapters;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Sponsors")]
    public class SponsorsActivity : CampActivityBase
    {
        private SponsorListAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Sponsors);

            var tiers = CodeCampApplication.CodeCampService.Repository.GetSponsorTiers();
            var sponsors = CodeCampApplication.CodeCampService.Repository.GetSponsors();
            var viewModel = new SponsorListViewModel(tiers, sponsors);

            var sponsorList = FindViewById<ListView>(Resource.Id.Sponsors);
            _adapter = new SponsorListAdapter(this, viewModel.Tiers);
            sponsorList.Adapter = _adapter;
            sponsorList.ItemClick += sponsorList_ItemClick;
        }

        void sponsorList_ItemClick(object sender, ItemEventArgs e)
        {
            var sponsor = _adapter.GetItemAtPosition(e.Position);
            var intent = new Intent();
            intent.SetClass(this, typeof (SponsorActivity));
            intent.PutExtra("name", sponsor.Name);
            intent.PutExtra("description", sponsor.Description);
            intent.PutExtra("website", sponsor.Website);

            StartActivity(intent);
        }
    }
}