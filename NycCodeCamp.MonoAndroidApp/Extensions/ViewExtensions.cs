using System;
using System.Collections.Generic;
using System.Linq;
using Android.Views;

namespace NycCodeCamp.MonoAndroidApp.Extensions
{
    public static class ViewExtensions
    {
        public static View ShowIf(this View view, bool show)
        {
            view.Visibility = show
                                ? ViewStates.Visible
                                : ViewStates.Gone;

            return view;
        }
    }
}