using System;
using Android.Content;
using Xamarin.ActionbarSherlockBinding.App;
using Android.OS;
using Xamarin.ActionbarSherlockBinding.Views;

namespace JiraJuggler
{
	public class BaseActivity : SherlockActivity
	{
	    private const string SettingsFileName = "JiraJugglerSettings";

	    protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Sherlock___Theme_DarkActionBar);
            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
			menu.Add(0, 0, 0, "About");
            menu.Add(0, 1, 0, "Settings");
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 1)
            {
                DisplayPreferencesActivity();
                return true;
            }

            return false;
        }

	    protected ISharedPreferences GetPreferences()
        {
            var context = (Context)this;
            return context.GetSharedPreferences(SettingsFileName, FileCreationMode.Private);
        }

	    protected void DisplayPreferencesActivity()
	    {
	        var intent = new Intent(this, typeof (JiraPreferenceActivity));
	        StartActivity(intent);
	    }

	    protected void CheckPreferencesAndDisplayScreenIfNotSet()
        {
            var preferences = GetPreferences();
            var jiraUrl = preferences.GetString("JiraUrl", null);
            var userName = preferences.GetString("UserName", null);
            var password = preferences.GetString("Password", null);

            if (String.IsNullOrEmpty(jiraUrl) || String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                DisplayPreferencesActivity();
            }
        }
	}
}