using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using TMDbLib.Client;
using TMDbLibMovie = TMDbLib.Objects.Movies;

namespace ArcTouch.Code.Challenge.Code
{
    class MovieAdapter : BaseAdapter<TMDbLibMovie.Movie>
    {
        string basePath = @"http://image.tmdb.org/t/p/w300";
        //My picture when null KKK
        string NoPicturePath = @"http://rutasmovil.azurewebsites.net/images/ups.png";

        public List<TMDbLibMovie.Movie> Items { get; set; }
     
        public MovieAdapter(int movieId) : base()
        {
            TMDbClient client = new TMDbClient("1f54bd990f1cdfb230adb312546d765d");
            TMDbLibMovie.Movie movie = client.GetMovieAsync(movieId).Result;
            Items = new List<TMDbLibMovie.Movie>
            {
                movie
            };

        }
        public override TMDbLibMovie.Movie this[int position]
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
            var view = convertView; 
            return view;
        }
        
    }
}

