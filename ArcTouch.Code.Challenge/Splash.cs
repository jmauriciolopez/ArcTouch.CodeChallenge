
using Android.App;
using Android.OS;

namespace ArcTouch.Code.Challenge
{ 
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, NoHistory = true, Exported = true)]
    public class Splash : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Threading.Thread.Sleep(100);
            StartActivity( typeof(MainActivity) );
        }
    }
}