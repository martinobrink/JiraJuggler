using System;
using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;
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

            //TODO: get all projects in jira:
            // GET https://jira.trifork.com/rest/api/2/project


			// Get our button from the layout resource,
			// and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate
            {
                //button.Text = string.Format ("{0} clicks!", 2*count++);
                button.Text = new JiraClient().PerformRequest();

                var imageIntent = new Intent();
                imageIntent.SetType("image/*");
                imageIntent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 0);
            };
		}

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                //TODO upload file data
                //var uri = data.Data;
                //var file = new File(uri.Path);
                //new JiraClient().UploadImage()

            }
        }
	}
}


