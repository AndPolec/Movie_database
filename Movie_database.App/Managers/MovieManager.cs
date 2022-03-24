using Movie_database.App.Concrete;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Managers
{
    public class MovieManager
    {
        private readonly MovieService _movieService;

        public MovieManager()
        {
            _movieService = new MovieService();
        }

        public bool IsMovieInDB(string title)
        {
            return _movieService.Items.Any(m => m.Title.ToLower() == title.ToLower()) ? true : false;
        }

        public void ShowMovieInfo(string title)
        {
            Movie movie = _movieService.Items.Find(m => m.Title.ToLower() == title.ToLower());
            Console.WriteLine("*******************");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Genere: {movie.Genere}");
            Console.WriteLine($"Rating: {movie.YourRating}/10");
            Console.WriteLine($"Relase year: {movie.RelaseYear}");
            Console.WriteLine($"Added to database: {movie.AdditionDate.ToString("g")}");
            Console.WriteLine("*******************");

        }

        public void AddMovie()
        {
            Console.WriteLine("Enter title of movie you want to add:");
            string inputTitle = Console.ReadLine();
            if (IsMovieInDB(inputTitle))
            {
                Console.WriteLine("Movie is already in database!");
            }
            else
            {
                Console.WriteLine("Select genere:");

                var generes = Enum.GetNames(typeof(Generes));
                for (int i = 0; i < generes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {generes[i]}");
                }

                int selectedGenereId = 0;
                while (selectedGenereId > generes.Length || selectedGenereId < 1)
                {
                    Console.WriteLine("Select: ");
                    selectedGenereId = int.Parse(Console.ReadKey().KeyChar.ToString());
                }
                Generes selectedGenere = (Generes)selectedGenereId;

                Console.WriteLine("Enter movie relase date:");
                int inputRelaseDate = 0;
                while (inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
                {
                    inputRelaseDate = int.Parse(Console.ReadLine()); ;
                    if (inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
                        Console.WriteLine("Incorrect date. Enter correct date:");
                }

                Console.WriteLine("Enter your movie rating (from 1 to 10) and press 'Enter':");
                int userRating = 0;
                while (userRating > 11 || userRating < 1)
                {
                    userRating = int.Parse(Console.ReadLine());
                    if (userRating > 11 || userRating < 1)
                        Console.WriteLine("Enter rating between 1 and 10");
                }

                _movieService.Items.Add(new Movie(_movieService.GetLastId()+1 ,inputTitle, selectedGenere, userRating, inputRelaseDate));
                Console.WriteLine($"Movie {inputTitle} added to base.");
            }

        }

        public void EditMovie(List<Menu> editMenu)
        {
            Console.WriteLine("Enter title of movie you want to edit:");
            string movieTitle = Console.ReadLine();
            if (!IsMovieInDB(movieTitle))
            {
                Console.WriteLine("Movie is not in the database.");
            }
            else
            {
                Movie movieToEdit = _movieService.Items.Find(m => m.Title.ToLower() == movieTitle.ToLower());
                ShowMovieInfo(movieToEdit.Title);

                foreach (var menu in editMenu)
                {
                    Console.WriteLine($"{menu.Id}. {menu.MenuAction}");
                }

                int select = 0;
                while (select > 4 || select < 1)
                {
                    Console.WriteLine("Select information you want to edit:");
                    select = int.Parse(Console.ReadKey().KeyChar.ToString());
                }

                switch (select)
                {
                    case 1:
                        Console.WriteLine("Enter new title:");
                        movieToEdit.Title = Console.ReadLine();
                        Console.WriteLine("Edited. New title: {0}", movieToEdit.Title);
                        break;

                    case 2:
                        Console.WriteLine("Select new genere:");
                        var generes = Enum.GetNames(typeof(Generes));
                        for (int i = 0; i < generes.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {generes[i]}");
                        }

                        int selectedGenereId = 0;
                        while (selectedGenereId > generes.Length || selectedGenereId < 1)
                        {
                            Console.WriteLine("Select: ");
                            selectedGenereId = int.Parse(Console.ReadKey().KeyChar.ToString());
                        }
                        movieToEdit.Genere = (Generes)selectedGenereId;
                        Console.WriteLine("Genere updated!");
                        break;

                    case 3:
                        Console.WriteLine("Enter your new rating:");
                        int userRating = 0;
                        while (userRating > 11 || userRating < 1)
                        {
                            userRating = int.Parse(Console.ReadLine());
                            if (userRating > 11 || userRating < 1)
                                Console.WriteLine("Enter rating between 1 and 10");
                        }
                        movieToEdit.YourRating = userRating;
                        Console.WriteLine("Rating updated");
                        break;

                    case 4:
                        Console.WriteLine("Enter relase year:");
                        int inputRelaseDate = 0;
                        while (inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
                        {
                            inputRelaseDate = int.Parse(Console.ReadLine()); ;
                            if (inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
                                Console.WriteLine("Incorrect date. Enter correct date:");
                        }
                        movieToEdit.RelaseYear = inputRelaseDate;
                        Console.WriteLine("Relase year updated");
                        break;

                    default:
                        Console.WriteLine("Incorrect option.");
                        break;
                }

            }
        }

        public void DeleteMovie()
        {
            Console.WriteLine("Enter title of movie you want to delete:");
            string titleOfMovieToDelete = Console.ReadLine();
            if (!IsMovieInDB(titleOfMovieToDelete))
            {
                Console.WriteLine("Movie is not in the database.");
            }
            else
            {
                ShowMovieInfo(titleOfMovieToDelete);
                _movieService.Remove(titleOfMovieToDelete);
                Console.WriteLine("Movie deleted");
            }
        }

        public void DisplayAllMovies()
        {
            Console.WriteLine("Movies in databse:");

            foreach (var movie in _movieService.Items)
            {
                Console.WriteLine();
                ShowMovieInfo(movie.Title);
                Console.WriteLine();
            }
        }

        public void DisplayRanking()
        {
            var rankList = _movieService.Items.OrderByDescending(m => m.YourRating).ToList();
            int position = 1;
            foreach (Movie movie in rankList)
            {
                Console.WriteLine();
                Console.WriteLine($"{position}.Title: {movie.Title}, Rating: {movie.YourRating}");
                position++;
            }
        }

    }
}
