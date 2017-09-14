using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using System;
using TMDbLib.Objects.General;

namespace ArcTouch.Code.Challenge.Code
{
    class MovieListAdapter : BaseAdapter<SearchMovie>
    {

        string basePath = @"http://image.tmdb.org/t/p/w300";
        string NoPicturePath = @"http://rutasmovil.azurewebsites.net/images/ups.png";

        private List<SearchMovie> Items { get; set; }
        private List<Genre> Genres { get; set; }

        TMDbClient client;
        public MovieListAdapter(int page, string searchCriteria) : base()
        {
            client = new TMDbClient("1f54bd990f1cdfb230adb312546d765d");

            Items = (searchCriteria == string.Empty) ?
                client.GetMovieUpcomingListAsync(null, page).Result.Results.ToList() :
                 client.SearchMovieAsync(searchCriteria, page).Result.Results.ToList()
                ;
            Genres = client.GetMovieGenresAsync().Result.ToList();

        }
        public override SearchMovie this[int position]
        {
            get { return Items[position]; }
        }

        public override int Count
        {
            get { return Items.Count; }
        }

        public override long GetItemId(int position)
        {
            return Items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            MovieHolder holder;
            var searchMovie = Items[position];
            var view = convertView;
            holder = (view != null) ? view.Tag as MovieHolder : new MovieHolder();

            if (view == null)
            {
                view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.MovieItem, null);
                holder.Title = view.FindViewById<TextView>(Resource.Id.MovieTitle);
                holder.ReleaseDate = view.FindViewById<TextView>(Resource.Id.ReleaseDate);
                holder.Genre = view.FindViewById<TextView>(Resource.Id.Genre);
                holder.Overview = view.FindViewById<TextView>(Resource.Id.Overview);
                holder.Poster = view.FindViewById<ImageView>(Resource.Id.Poster);
                view.Tag = holder;
            }
            holder.Overview.Visibility = ViewStates.Gone;

            holder.Title.Text = searchMovie.Title;
            holder.ReleaseDate.Text = (searchMovie.ReleaseDate != null) ? ((DateTime)searchMovie.ReleaseDate).ToShortDateString() : "";

            holder.Genre.Text = (searchMovie.GenreIds.Count() > 0) ? Genres.Find(g => g.Id == searchMovie.GenreIds.FirstOrDefault()).Name : "No Data";
            var url = (searchMovie.PosterPath != null) ?
                $"{basePath}{searchMovie.PosterPath}" :
                (searchMovie.BackdropPath != null) ? $"{basePath}{searchMovie.BackdropPath}"
                : NoPicturePath;
            Koush.UrlImageViewHelper.SetUrlDrawable(holder.Poster, url);
            return view;
        }
        private class MovieHolder : Java.Lang.Object
        {
            public TextView Title { get; set; }
            public TextView ReleaseDate { get; set; }
            public TextView Genre { get; set; }
            public TextView Overview { get; set; }
            public ImageView Poster { get; set; }
        }


    }
}

