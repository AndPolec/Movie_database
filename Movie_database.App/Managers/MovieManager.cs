using Movie_database.App.Concrete;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Managers
{
    public class MovieManager
    {
        private readonly MovieService _movieService;
        private string[] _generes;

        public MovieManager(MovieService service)
        {
            _movieService = service;
            _generes = Enum.GetNames(typeof(Generes));
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
            Console.WriteLine("*******************");

        }

        public int AddMovie()
        {
            Console.WriteLine("\nEnter title of movie you want to add:");
            string title = Console.ReadLine();

            if (IsMovieInDB(title))
            {
                Console.WriteLine("Movie is already in database!");
                return -1;
            }

            Console.WriteLine("Select genere:");
            Generes genere = SelectGenere();

            Console.WriteLine("\nEnter movie relase date (1985-{0}):", DateTime.Today.Year);
            int relaseDate = SelectRelaseDate();
                
            Console.WriteLine("Enter your movie rating (from 1 to 10):");
            int rating = SelectRating();

            var movie = new Movie(_movieService.GetLastId() + 1, title, genere, rating, relaseDate);
            Console.WriteLine($"Movie {title} added to base.");

            return _movieService.Add(movie);
        }

        public void EditMovie(List<Menu> editMenu)
        {
            Console.WriteLine("\nEnter title of movie you want to edit:");
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
                    select = int.Parse(Console.ReadLine());
                }

                switch (select)
                {
                    case 1:
                        Console.WriteLine("\nEnter new title:");
                        movieToEdit.Title = Console.ReadLine();
                        Console.WriteLine("Edited. New title: {0}", movieToEdit.Title);
                        break;

                    case 2:
                        Console.WriteLine("\nSelect new genere:");
                        var generes = Enum.GetNames(typeof(Generes));
                        for (int i = 0; i < generes.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {generes[i]}");
                        }

                        int selectedGenereId = 0;
                        while (selectedGenereId > generes.Length || selectedGenereId < 1)
                        {
                            Console.WriteLine("Select: ");
                            selectedGenereId = int.Parse(Console.ReadLine());
                        }
                        movieToEdit.Genere = (Generes)selectedGenereId;
                        Console.WriteLine("Genere updated!");
                        break;

                    case 3:
                        Console.WriteLine("\nEnter your new rating:");
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
                        Console.WriteLine("\nEnter relase year:");
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
                        Console.WriteLine("\nIncorrect option.");
                        break;
                }

            }
        }

        public void DeleteMovie()
        {
            Console.WriteLine("\nEnter title of movie you want to delete:");
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

        public void DisplayMovies(List<Menu> displayMenu)
        {
            Console.WriteLine();
            foreach (var menu in displayMenu)
            {
                Console.WriteLine($"{menu.Id}. {menu.MenuAction}");
            }

            int select = 0;
            while (select > 4 || select < 1)
            {
                Console.WriteLine("Select:");
                select = int.Parse(Console.ReadKey().KeyChar.ToString());
            }

            switch (select)
            {
                case 1:
                    Console.WriteLine("\nMovies in database:");

                    foreach (var movie in _movieService.Items.OrderBy(m => m.Title).ToList())
                    {
                        Console.WriteLine();
                        ShowMovieInfo(movie.Title);
                        Console.WriteLine();
                    }
                    break;

                case 2:
                    Console.WriteLine("\nSelect type of movie you want to display:");
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

                    List<Movie> listByGenere = _movieService.Items.FindAll(m => m.Genere == selectedGenere);
                    if (listByGenere.Count == 0)
                    {
                        Console.WriteLine("There are no movies from the selected genere in the database.");
                    }
                    else
                    {
                        foreach (var movie in listByGenere)
                        {
                            Console.WriteLine();
                            ShowMovieInfo(movie.Title);
                            Console.WriteLine();
                        }
                    }
                    break;

                case 3:
                    Console.WriteLine("\nEnter movie relase date:");
                    int inputRelaseDate = 0;
                    inputRelaseDate = int.Parse(Console.ReadLine());

                    List<Movie> listByYear = _movieService.Items.FindAll(m => m.RelaseYear == inputRelaseDate);

                    if (listByYear.Count==0)
                    {
                        Console.WriteLine("There are no movies from the selected year in the database.");
                    }
                    else
                    {
                        foreach (var movie in listByYear)
                        {
                            Console.WriteLine();
                            ShowMovieInfo(movie.Title);
                            Console.WriteLine();
                        }
                    }
                    break;

                default:
                    Console.WriteLine("\nIncorrect option.");
                    break;
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

        public int TakeNumberFromUser(string typeOfNumber)
        {
            string input;
            int resultNumber = 0;

            switch (typeOfNumber)
            {
                case "genere":
                    while (true)
                    {
                        Console.WriteLine("Select: ");
                        input = Console.ReadLine();

                        if(!input.Any(n => n < '0' || n > '9'))
                        {
                            if(Int32.Parse(input) >= 1 && Int32.Parse(input) <= _generes.Length)
                            {
                                resultNumber = Int32.Parse(input);
                                break;
                            }
                        }
                    }
                    break;

                default:
                    break;
            }

            return resultNumber;
        }

        private Generes SelectGenere()
        {
            for (int i = 0; i < _generes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {_generes[i]}");
            }

            int selectedGenereNumber = -1;
            Console.WriteLine("Select: ");

            while (selectedGenereNumber == -1)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();

                if (char.IsDigit(userInput.KeyChar) && int.Parse(userInput.KeyChar.ToString()) >= 1 && int.Parse(userInput.KeyChar.ToString()) <= _generes.Length)
                    selectedGenereNumber = int.Parse(userInput.KeyChar.ToString());
            }
            
            return (Generes)selectedGenereNumber;
        }
    
        private int SelectRelaseDate()
        {
            int inputRelaseDate = 0;
            while (inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
            {
                inputRelaseDate = int.Parse(Console.ReadLine());
            }

            return inputRelaseDate;
        }
        
        private int SelectRating()
        {
            int rating = 0;
            while (rating > 11 || rating < 1)
            {
                rating = int.Parse(Console.ReadLine());
            }

            return rating;
        }
    
    }
}
