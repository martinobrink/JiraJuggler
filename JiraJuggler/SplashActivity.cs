using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace JiraJuggler
{
    //TODO fix intent filter
    [Activity(Label = "JiraJuggler", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    [IntentFilter(
        new[] { Intent.ActionView }, 
        Categories = new[] { Intent.ActionDefault, Intent.CategoryBrowsable, Intent.ActionSend, Intent.ActionSendMultiple, Intent.CategoryDefault }, 
        DataScheme = "mimetype",
        DataPathPattern = "*/*",
        DataHost = "*.*")]
    public class SplashActivity : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            Intent intent = Intent;
            String action = intent.Action;
            String type = intent.Type;

            if (Intent.ActionSend.Equals(action) && type != null)
            {
                if (type.StartsWith("image/"))
                {
                    //TODO upload file to jira
                }
            }
            else
            {
                // Start our real activity if no acceptable file received
                StartActivity(typeof(MainActivity));
            }
        }
    }
}