using System;
using Xamarin.ActionbarSherlockBinding.App;
using Android.OS;
using Xamarin.ActionbarSherlockBinding.Views;

namespace JiraJuggler
{
	public class BaseActivity : SherlockActivity
	{
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme (Resource.Style.Sherlock___Theme_DarkActionBar);
            base.OnCreate(savedInstanceState);
        }

        public override bool OnCreateOptionsMenu(Xamarin.ActionbarSherlockBinding.Views.IMenu menu)
        {
			menu.Add(0,0,0, "About");
            return base.OnCreateOptionsMenu(menu);
        }
	}
}

