using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    [Activity(Label = "Tags")]
    public class TagsActivity : TopLevelCampActivityBase
    {
        private ListView _tagList;
        private IList<string> _tags; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tags);

            _tags = CodeCampApplication.CodeCampService.Repository.GetTags().OrderBy(tag => tag).ToList();
            _tagList = FindViewById<ListView>(Resource.Id.Tags);
            _tagList.Adapter = new ArrayAdapter<string>(this, Resource.Layout.TagsItem, Resource.Id.TagName, _tags);
            _tagList.ItemClick += _tagList_ItemClick;
        }

        void _tagList_ItemClick(object sender, ItemEventArgs e)
        {
            var intent = new Intent();
            intent.SetClass(this, typeof (SessionsByTagActivity));
            intent.PutExtra("tag", _tags[e.Position]);

            StartActivity(intent);
        }
    }
}