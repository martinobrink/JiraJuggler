using System;
using Android;
using Android.Content;
using Xamarin.ActionbarSherlockBinding.App;
using Android.OS;
using Xamarin.ActionbarSherlockBinding.Views;

namespace JiraJuggler
{
	public class BaseActivity : SherlockActivity
	{
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(Resource.Style.Sherlock___Theme_DarkActionBar);
            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(Xamarin.ActionbarSherlockBinding.Views.IMenu menu)
        {
			menu.Add(0, 0, 0, "About");
            menu.Add(0, 1, 0, "Settings");
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 1)
            {
                var intent = new Intent(this, typeof (JiraPreferenceActivity));
                StartActivity(intent);
                return true;
            }

            return false;
        }
	}
}

