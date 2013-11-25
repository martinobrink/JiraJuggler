using System;
using System.IO;
using System.Net;
using System.Reactive.Linq;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Widget;
using Android.OS;
using JiraJuggler.Shared;
using Observable = System.Reactive.Linq.Observable;

namespace JiraJuggler
{
	[Activity (Label = "JiraJuggler")]
	public class MainActivity : BaseActivity
	{
	    private JiraClient _jiraClient;

	    protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetTitle (Resource.String.app_name);
			SetContentView (Resource.Layout.Main);

		    Button jiraProjectsButton = FindViewById<Button>(Resource.Id.myButton);
		    Button selectFileButton = FindViewById<Button>(Resource.Id.buttonSelectFile);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.projectSpinner);
		    var projectListArrayAdapter = new ProjectListArrayAdapter(this);
            spinner.Adapter = projectListArrayAdapter;

            var jiraProjectsButtonClick = Observable.FromEventPattern(
                h => jiraProjectsButton.Click += h,
                h => jiraProjectsButton.Click -= h);

	        var jiraProjectsReceived = from _ in jiraProjectsButtonClick
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


	        selectFileButton.Click += (sender, args) =>
	            {
	                //intent for picking image from gallery
	                var imageIntent = new Intent();
	                imageIntent.SetType("image/*");
	                imageIntent.SetAction(Intent.ActionGetContent);
	                StartActivityForResult(Intent.CreateChooser(imageIntent, "Select photo"), 0);
	            };
		}

        protected override void OnResume()
        {
            base.OnResume();
            
            var preferences = PreferenceManager.GetDefaultSharedPreferences(Application.ApplicationContext);
            var jiraUrl = preferences.GetString("JiraUrl", null);
            var userName = preferences.GetString("UserName", null);
            var password = preferences.GetString("Password", null);

            if (String.IsNullOrEmpty(jiraUrl) || String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                DisplayPreferencesActivity();
            }

            _jiraClient = new JiraClient(jiraUrl, userName, password);
        }

        protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode != Result.Ok)
            {
                return;
            }

            var fileUri = data.Data;
            var filePath = GetFilePathFromMediaUri(fileUri);
            var fileName = new FileInfo(filePath).Name;
            var uploadResponse = await _jiraClient.UploadImage("DBN-39", File.ReadAllBytes(filePath), fileName);
            if (uploadResponse.StatusCode == HttpStatusCode.OK)
            {
                Toast.MakeText(this, "File '" + fileName + "' successfully uploaded.", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, string.Format("An error occured: {0} ({1}).", uploadResponse.ReasonPhrase, uploadResponse.StatusCode), ToastLength.Long).Show();
            }
        }

        private string GetFilePathFromMediaUri(Android.Net.Uri uri)
        {
            string path = null;
            var projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
            using (var cursor = ManagedQuery(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
	}
}