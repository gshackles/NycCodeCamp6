using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;

namespace NycCodeCamp.MonoAndroidApp.Activities
{
    public abstract class CampActivityBase : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.SetBackgroundDrawableResource(Resource.Drawable.WindowBackground);
        }
    }
}