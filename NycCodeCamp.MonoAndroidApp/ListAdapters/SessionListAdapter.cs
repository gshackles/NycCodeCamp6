using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using CodeCamp.Core.Entities;
using CodeCamp.Core.ViewModels;

namespace NycCodeCamp.MonoAndroidApp.ListAdapters
{
    public class SessionListAdapter : SectionListAdaperBase<FullScheduleViewModel.TimeSlot, Session>
    {
        public SessionListAdapter(Activity context, IList<FullScheduleViewModel.TimeSlot> slots)
            : base(context, slots, slot => slot.Description, slot => slot.Sessions, Resource.Layout.SessionsItem)
        {
        }

        protected override void PopulateItemView(View view, Session item)
        {
            view.FindViewById<TextView>(Resource.Id.Title).Text = item.Title;
            view.FindViewById<TextView>(Resource.Id.SpeakerName).Text = item.Speaker.Name;
            view.FindViewById<TextView>(Resource.Id.Room).Text = item.Room;
        }
    }
}