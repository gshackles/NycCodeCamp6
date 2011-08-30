using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using CodeCamp.Core.Entities;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Speakers")]
    public class SpeakersActivity : CampActivityBase
    {
        private ListView _speakerList;
        private IList<Speaker> _speakers; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Speakers);

            _speakers =
                CodeCampApplication.CodeCampService.Repository.GetSpeakers().OrderBy(speaker => speaker.Name).ToList();
            var speakerNames = _speakers.Select(speaker => speaker.Name).ToList();

            _speakerList = FindViewById<ListView>(Resource.Id.Speakers);
            _speakerList.Adapter = new ArrayAdapter<string>(this, Resource.Layout.SpeakersItem, Resource.Id.SpeakerName,
                                                            speakerNames);
            _speakerList.ItemClick += _speakerList_ItemClick;
        }

        void _speakerList_ItemClick(object sender, ItemEventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof (SpeakerActivity));
            intent.PutExtra("email", _speakers[e.Position].Email);

            StartActivity(intent);
        }
    }
}