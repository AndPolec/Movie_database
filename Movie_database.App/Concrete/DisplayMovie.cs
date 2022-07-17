using Movie_database.App.Abstract;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_database.App.Concrete
{
    public class DisplayMovie : IDisplayMovie
    {
        public IService<Movie> Service { get;}

        public DisplayMovie(IService<Movie> movieService)
        {
            Service = movieService;
        }

        public void MovieInfo(string title)
        {
            Movie movie = Service.Items.Find(m => m.Title.ToLower() == title.ToLower());
            Console.WriteLine("*******************");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Genere: {movie.Genere}");
            Console.WriteLine($"Rating: {movie.YourRating}/10");
            Console.WriteLine($"Relase year: {movie.RelaseYear}");
            Console.WriteLine("*******************");

        }

        public void Ranking()
        {
            var rankList = Service.Items.OrderByDescending(m => m.YourRating).ToList();
            int position = 1;
            foreach (Movie movie in rankList)
            {
                Console.WriteLine();
                Console.WriteLine($"{position}.Title: {movie.Title}, Rating: {movie.YourRating}");
                position++;
            }
        }

        public void AllMovies()
        {
            foreach (var movie in Service.Items.OrderBy(m => m.Title).ToList())
            {
                Console.WriteLine();
                MovieInfo(movie.Title);
                Console.WriteLine();
            }
        }

        public void MoviesByGenere(Generes genere)
        {
            List<Movie> listByGenere = Service.Items.FindAll(m => m.Genere == genere);
            if (listByGenere.Count == 0)
            {
                Console.WriteLine("There are no movies from the selected genere in the database.");
            }
            else
            {
                foreach (var movie in listByGenere)
                {
                    Console.WriteLine();
                    MovieInfo(movie.Title);
                    Console.WriteLine();
                }
            }
        }

        public void MoviesByRelaseYear(int selectedYear)
        {
            List<Movie> listByYear = Service.Items.FindAll(m => m.RelaseYear == selectedYear);

            if (listByYear.Count == 0)
            {
                Console.WriteLine("There are no movies from the selected year in the database.");
            }
            else
            {
                foreach (var movie in listByYear)
                {
                    Console.WriteLine();
                    MovieInfo(movie.Title);
                    Console.WriteLine();
                }
            }
        }


       
    }
}
