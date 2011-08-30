using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    public abstract class CampActivityBase : Activity
    {
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            base.OnCreateOptionsMenu(menu);
            
            new MenuInflater(this).Inflate(Resource.Menu.MainOptionsMenu, menu);

            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            menu.FindItem(Resource.Id.Update).SetVisible(this is OverviewActivity);
            menu.FindItem(Resource.Id.Overview).SetVisible(!(this is OverviewActivity));
            menu.FindItem(Resource.Id.Rooms).SetVisible(!(this is RoomsActivity));
            menu.FindItem(Resource.Id.Sessions).SetVisible(!(this is SessionsActivity));
            menu.FindItem(Resource.Id.Speakers).SetVisible(!(this is SpeakersActivity));
            menu.FindItem(Resource.Id.Sponsors).SetVisible(!(this is SponsorsActivity));
            menu.FindItem(Resource.Id.Tags).SetVisible(!(this is TagsActivity));

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Update:
                    CodeCampApplication.CodeCampService.CheckForUpdatedSchedule();
                    break;
                case Resource.Id.Overview:
                    StartActivity(typeof(OverviewActivity));
                    break;
                case Resource.Id.Rooms:
                    StartActivity(typeof(RoomsActivity));
                    break;
                case Resource.Id.Sessions:
                    StartActivity(typeof(SessionsActivity));
                    break;
                case Resource.Id.Speakers:
                    StartActivity(typeof(SpeakersActivity));
                    break;
                case Resource.Id.Sponsors:
                    StartActivity(typeof(SponsorsActivity));
                    break;
                case Resource.Id.Tags:
                    StartActivity(typeof(TagsActivity));
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }

            return true;
        }
    }
}