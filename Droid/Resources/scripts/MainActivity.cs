﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MySpringbrook.Droid
{
	[Activity(Label = "MySpringbrook.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

        public static string aOrAn(string input)
        {
            input = input.ToLower();
            if (input.StartsWith("a") || input.StartsWith("e") || input.StartsWith("i") || input.StartsWith("o") || input.StartsWith("u"))
                return "an";
            else
                return "a";
        }
	}
}
