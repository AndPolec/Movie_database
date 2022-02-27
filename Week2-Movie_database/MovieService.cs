namespace Week2_Movie_database
{
    public class MovieService
    {
        private List<Movie> _movies = new List<Movie>();

        public bool IsMovieInDB(string title)
        {
            return _movies.Any(m => m.Title.ToLower() == title.ToLower()) ? true : false;
        }

        public void ShowMovieInfo(string title)
        {
            Movie movie = _movies.Find(m => m.Title.ToLower() == title.ToLower());
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
                        Console.WriteLine("Incorret date. Enter correct date:");
                }

                Console.WriteLine("Enter your movie rating (from 1 to 10) and press 'Enter':");
                int userRating = 0;
                while (userRating > 11 || userRating < 1)
                {
                    userRating = int.Parse(Console.ReadLine());
                    if (userRating > 11 || userRating < 1)
                        Console.WriteLine("Enter rating between 1 and 10");
                }

                _movies.Add(new Movie(inputTitle, selectedGenere, userRating, inputRelaseDate));
                Console.WriteLine($"Movie {inputTitle} added to base.");
            }

        }

        public void EditMovie(MenuService menuService, MovieService movieService)
        {
            Console.WriteLine("Enter title of movie you want to edit:");
            string movieTitle = Console.ReadLine();
            if (!IsMovieInDB(movieTitle))
            {
                Console.WriteLine("Movie is not in the database.");
            }
            else
            {
                Movie movieToEdit = _movies.Find(m => m.Title == movieTitle);
                ShowMovieInfo(movieToEdit.Title);

                var editMenu = menuService.GetMenuByMenuName("Edit movie");
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
                    case '1':
                        break;
                    case '2':
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
                        Console.WriteLine("Genere update!");
                        break;
                    case '3':
                        break;
                    case '4':
                        break;

                    default:
                        break;
                }

            }
        }


    }
}
