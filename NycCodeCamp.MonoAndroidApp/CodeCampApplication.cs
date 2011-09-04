using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using CodeCamp.Core.DataAccess;

namespace NycCodeCamp.MonoAndroidApp
{
    [Application(Theme = "@style/ApplicationTheme", Label = "@string/ApplicationName")]
    public class CodeCampApplication : Application
    {
        public static CodeCampService CodeCampService { get; set; }

        public CodeCampApplication(IntPtr handle)
            : base(handle)
        {
        }
    }
}