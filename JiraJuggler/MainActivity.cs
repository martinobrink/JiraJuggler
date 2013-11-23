using System;
using System.Reactive.Linq;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Widget;
using Android.OS;
using JiraJuggler.Shared;

namespace JiraJuggler
{
	[Activity (Label = "JiraJuggler", MainLauncher = true)]
	public class MainActivity : BaseActivity
	{
	    private JiraClient _jiraClient;

	    protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetTitle (Resource.String.app_name);
            
			SetContentView (Resource.Layout.Main);

            //TODO: maybe put this in OnResume() so settings are reloaded after returning from SettingsActivity
            //TODO: Notify user of settings not found or show preferences activity
		    var preferences = PreferenceManager.GetDefaultSharedPreferences(this);
		    var jiraUrl = preferences.GetString("JiraUrl", null); 
            var userName = preferences.GetString("UserName", null); 
            var password = preferences.GetString("Password", null);
            _jiraClient = new JiraClient(jiraUrl, userName, password);

		    Button button = FindViewById<Button>(Resource.Id.myButton);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.projectSpinner);
		    var projectListArrayAdapter = new ProjectListArrayAdapter(this);
            spinner.Adapter = projectListArrayAdapter;

            var buttonClick = Observable.FromEventPattern(
                h => button.Click += h,
                h => button.Click -= h);

	        var jiraProjectsReceived = from _ in buttonClick
	                                   from projects in _jiraClient.GetProjects()
	                                   select projects;

		    jiraProjectsReceived
                .Subscribe(
                    jiraProjects => 
                        RunOnUiThread(() =>
		                {
                            projectListArrayAdapter.Clear();
		                    projectListArrayAdapter.AddAll(jiraProjects);
		                    spinner.SetSelection(0, true);
		                }), 
                    exception => 
                        RunOnUiThread(() => 
                            Toast.MakeText(this, "An error occured: '" + exception.Message + "'. Did you forget to set jira configuration under Settings?", ToastLength.Long).Show()
                    ));

            //intent for picking image from gallery
            //var imageIntent = new Intent();
            //imageIntent.SetType("image/*");
            //imageIntent.SetAction(Intent.ActionGetContent);
            //StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 0);
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


