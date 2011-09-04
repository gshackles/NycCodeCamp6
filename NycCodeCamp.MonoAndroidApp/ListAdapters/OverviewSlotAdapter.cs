using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using CodeCamp.Core.Entities;
using CodeCamp.Core.ViewModels;

namespace NycCodeCamp.MonoAndroidApp.ListAdapters
{
    public class OverviewSlotAdapter : SectionListAdaperBase<CampOverviewViewModel.TimeSlot, Session>
    {
        public OverviewSlotAdapter(Activity context, IList<CampOverviewViewModel.TimeSlot> slots)
            : base(context, slots, slot => slot.Description, slot => slot.Sessions, Resource.Layout.OverviewItem)
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