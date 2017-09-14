using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using ArcTouch.Code.Challenge.Code;
using Android.Content;
using Android.Views;
using System.Threading.Tasks;

namespace ArcTouch.Code.Challenge
{
    [Activity(Label = "@string/app_name", MainLauncher = false, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        Toolbar toolbar;
        ListView MainGridListview;
        MovieListAdapter adap;
        Button toolbarmenuarrowback;
        Button toolbarmenuarrowforward;
        int page = 1;
        EditText SearchCriteria;
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainGridScreen);
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "ArcTouch";
            MainGridListview = FindViewById<ListView>(Resource.Id.MainGridListview);
            MainGridListview.ChoiceMode = ChoiceMode.Single;
            MainGridListview.ItemClick += MainGridListview_ItemClick;
            MainGridListview.FastScrollEnabled = true;

            toolbarmenuarrowback = FindViewById<Button>(Resource.Id.toolbarmenuarrowback);
            toolbarmenuarrowback.Click += Toolbarmenuarrowback_Click;
            toolbarmenuarrowforward = FindViewById<Button>(Resource.Id.toolbarmenuarrowforward);
            toolbarmenuarrowforward.Click += Toolbarmenuarrowforward_Click;

            SearchCriteria = FindViewById<EditText>(Resource.Id.SearchCriteria);
            SearchCriteria.KeyPress += SearchCriteria_KeyPress;

            using (var progressdialog = new ProgressDialog(this))
            {
                progressdialog.SetCancelable(false);
                progressdialog.Indeterminate = true;
                progressdialog.SetMessage("Loading...");
                progressdialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                progressdialog.Show();
                await RefreshList(page, SearchCriteria.Text);
                progressdialog.Dismiss();
            }

        }

        private async void SearchCriteria_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
            {
                page = 1;
                await RefreshList(page, SearchCriteria.Text);
            }
            else
                e.Handled = false;
        }

        private async void Toolbarmenuarrowforward_Click(object sender, EventArgs e)
        {
            page++;
            await RefreshList(page, SearchCriteria.Text);
        }

        private async void Toolbarmenuarrowback_Click(object sender, EventArgs e)
        {
            if (page > 1)
                page--;
            else return;
            await RefreshList(page, SearchCriteria.Text);
        }

        private void MainGridListview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var movie = ((MovieListAdapter)MainGridListview.Adapter)[e.Position];
            var movieActivity = new Intent(this, typeof(MovieActivity));
            movieActivity.PutExtra("movieId", movie.Id.ToString());
            StartActivity(movieActivity);

        }

        public async Task<bool> RefreshList(int page, string searchCriteria = null)
        {
            adap = new MovieListAdapter(page, searchCriteria);
            MainGridListview.Adapter = adap;
            return true;

        }

    }
}

