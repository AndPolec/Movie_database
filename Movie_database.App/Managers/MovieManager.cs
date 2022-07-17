using Movie_database.App.Abstract;
using Movie_database.App.Concrete;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Managers
{
    public class MovieManager
    {
        private readonly MovieService _movieService;
        private IUserInput _userInput;
        private IDisplayMovie _display;
        private string[] _generes;

        public MovieManager(MovieService service, IUserInput userInput, IDisplayMovie display)
        {
            _movieService = service;
            _generes = Enum.GetNames(typeof(Generes));
            _userInput = userInput;
            _display = display;
        }

        public bool IsMovieInDB(string title)
        {
            return _movieService.Items.Any(m => m.Title.ToLower() == title.ToLower()) ? true : false;
        }

        public void AddMovie()
        {
            Console.WriteLine("\nEnter title of movie you want to add:");
            string title = _userInput.SelectTitle();

            if (IsMovieInDB(title))
            {
                Console.WriteLine("Movie is already in database!");
            }
            else
            {

                Console.WriteLine("Select genere:");
                Generes genere = _userInput.SelectGenere(_generes);

                Console.WriteLine("\nEnter movie relase date (1985-{0}):", DateTime.Today.Year);
                int relaseDate = _userInput.SelectRelaseDate();

                Console.WriteLine("Enter your movie rating (from 1 to 10):");
                int rating = _userInput.SelectRating();

                var movie = new Movie(_movieService.GetLastId() + 1, title, genere, rating, relaseDate);
                Console.WriteLine($"Movie {title} added to base.");

                _movieService.Add(movie);

            }
        }

        public void EditMovie(List<Menu> editMenu)
        {
            Console.WriteLine("\nEnter title of movie you want to edit:");
            string movieTitle = _userInput.SelectTitle();
            if (!IsMovieInDB(movieTitle))
            {
                Console.WriteLine("Movie is not in the database.");
            }
            else
            {
                Movie movieToEdit = _movieService.Items.Find(m => m.Title.ToLower() == movieTitle.ToLower());
                _display.MovieInfo(movieToEdit.Title);

                int select = _userInput.SelectMenuOption(editMenu);

                switch (select)
                {
                    case 1:
                        Console.WriteLine("\nEnter new title:");
                        movieToEdit.Title = _userInput.SelectTitle();
                        Console.WriteLine("Edited. New title: {0}", movieToEdit.Title);
                        break;

                    case 2:
                        Console.WriteLine("\nSelect new genere:");
                        movieToEdit.Genere = _userInput.SelectGenere(_generes);
                        Console.WriteLine("Genere updated!");
                        break;

                    case 3:
                        Console.WriteLine("\nEnter your new rating:");
                        movieToEdit.YourRating = _userInput.SelectRating();
                        Console.WriteLine("Rating updated");
                        break;

                    case 4:
                        Console.WriteLine("\nEnter relase year:");
                        movieToEdit.RelaseYear = _userInput.SelectRelaseDate();
                        Console.WriteLine("Relase year updated");
                        break;

                    default:
                        Console.WriteLine("\nIncorrect option.");
                        break;
                }

            }
        }

        public void DeleteMovie()
        {
            Console.WriteLine("\nEnter title of movie you want to delete:");
            string titleOfMovieToDelete = _userInput.SelectTitle();
            if (!IsMovieInDB(titleOfMovieToDelete))
            {
                Console.WriteLine("Movie is not in the database.");
            }
            else
            {
                _display.MovieInfo(titleOfMovieToDelete);
                _movieService.Remove(titleOfMovieToDelete);
                Console.WriteLine("Movie deleted");
            }
        }

        public void DisplayMovies(List<Menu> displayMenu)
        {
            Console.WriteLine();

            int select = _userInput.SelectMenuOption(displayMenu);

            switch (select)
            {
                case 1:
                    Console.WriteLine("\nMovies in database:");
                    _display.AllMovies();
                    break;

                case 2:
                    Console.WriteLine("\nSelect type of movie you want to display:");
                    Generes selectedGenere = _userInput.SelectGenere(_generes);
                    _display.MoviesByGenere(selectedGenere);
                    break;

                case 3:
                    Console.WriteLine("\nEnter movie relase date:");
                    int selectedRelaseDate = _userInput.SelectRelaseDate();
                    _display.MoviesByRelaseYear(selectedRelaseDate);
                    break;

                default:
                    Console.WriteLine("\nIncorrect option.");
                    break;
            }

        
        }

        public void DisplayRanking()
        {
            _display.Ranking();
        }
    }
}
