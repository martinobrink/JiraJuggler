using Android.App;
using Android.OS;
using Android.Preferences;

namespace JiraJuggler
{
    [Activity(Label = "Settings")]
    public class JiraPreferenceActivity : PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AddPreferencesFromResource(Resource.Xml.PreferenceScreen);
        }
    }
}