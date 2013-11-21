using System;
using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using JiraJuggler.Shared;
using Xamarin.ActionbarSherlockBinding.App;

namespace JiraJuggler
{
	[Activity (Label = "JiraJuggler", MainLauncher = true)]
	public class MainActivity : BaseActivity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetTitle (Resource.String.app_name);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);



			// Get our button from the layout resource,
			// and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                //button.Text = string.Format ("{0} clicks!", 2*count++);
                button.Text = new JiraClient().PerformRequest();
            };
		}
	}
}


