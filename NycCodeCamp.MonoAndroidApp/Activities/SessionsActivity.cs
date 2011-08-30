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
    [Activity(Label = "Sessions")]
    public class SessionsActivity : CampActivityBase
    {
        private SessionListAdapter _adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Sessions);

            var allSessions = CodeCampApplication.CodeCampService.Repository.GetSessions();
            var viewModel = new FullScheduleViewModel(allSessions);

            var sessionsList = FindViewById<ListView>(Resource.Id.Sessions);
            _adapter = new SessionListAdapter(this, viewModel.Schedule);
            sessionsList.Adapter = _adapter;
            sessionsList.ItemClick += sessionsList_ItemClick;
        }

        private void sessionsList_ItemClick(object sender, ItemEventArgs e)
        {
            var session = _adapter.GetItemAtPosition(e.Position);

            var intent = new Intent();
            intent.SetClass(this, typeof(SessionActivity));
            intent.PutExtra("key", session.Key);

            StartActivity(intent);
        }
    }
}