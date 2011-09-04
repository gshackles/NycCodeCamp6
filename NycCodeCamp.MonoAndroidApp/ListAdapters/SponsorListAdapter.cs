using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using CodeCamp.Core.Entities;
using CodeCamp.Core.ViewModels;

namespace NycCodeCamp.MonoAndroidApp.ListAdapters
{
    public class SponsorListAdapter : SectionListAdaperBase<SponsorListViewModel.Tier, Sponsor>
    {
        public SponsorListAdapter(Activity context, IList<SponsorListViewModel.Tier> tiers)
            : base(context, tiers, tier => tier.Name, tier => tier.Sponsors, Resource.Layout.SponsorsItem)
        {
        }

        protected override void PopulateItemView(View view, Sponsor item)
        {
            view.FindViewById<TextView>(Resource.Id.SponsorName).Text = item.Name;
        }
    }
}