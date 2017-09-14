using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ArcTouch.Code.Challenge.Code;

namespace ArcTouch.Code.Challenge
{
    [Activity(Label = "MovieActivity")]
    public class MovieActivity : Activity
    {
        TextView MovieTitle { get; set; }
        TextView ReleaseDate { get; set; }
        TextView Genre { get; set; }
        TextView Overview { get; set; }
        ImageView Poster { get; set; }

        string basePath = @"http://image.tmdb.org/t/p/w300";
        //My picture when null KKK
        string NoPicturePath = @"http://rutasmovil.azurewebsites.net/images/ups.png";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string movieId = Intent.GetStringExtra("movieId") ?? "0";
            SetContentView(Resource.Layout.MovieItem);

            MovieTitle = FindViewById<TextView>(Resource.Id.MovieTitle);
            ReleaseDate = FindViewById<TextView>(Resource.Id.ReleaseDate);
            Genre = FindViewById<TextView>(Resource.Id.Genre);
            Overview = FindViewById<TextView>(Resource.Id.Overview);
            Poster = FindViewById<ImageView>(Resource.Id.Poster);

            var adap = new MovieAdapter(Convert.ToInt32(movieId));
            var item = adap.Items[0];

            MovieTitle.Text = item.Title;
            ReleaseDate.Text = (item.ReleaseDate != null) ? ((DateTime)item.ReleaseDate).ToShortDateString():"";
            Genre.Text = (item.Genres.Count() > 0) ? item.Genres.FirstOrDefault().Name : "No data";
            Overview.Text = item.Overview ?? "No info provided yet.";
            var url = (item.PosterPath != null) ?
              $"{basePath}{item.PosterPath}" :
              (item.BackdropPath != null) ? $"{basePath}{item.BackdropPath}"
              : NoPicturePath;
            Koush.UrlImageViewHelper.SetUrlDrawable(Poster, url);


        }
    }
}