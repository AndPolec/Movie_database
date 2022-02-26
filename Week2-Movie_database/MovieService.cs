namespace Week2_Movie_database
{
    public class MovieService
    {
        private List<Movie> _movies = new List<Movie>();

       

        public void AddMovie()
        {
            Console.WriteLine("Enter title of movie you want to add:");
            string inputTitle = Console.ReadLine();
            while(_movies.Any(m => m.Title.ToLower() == inputTitle.ToLower()))
            {
                Console.WriteLine("Movie is already in database.");
                Console.WriteLine("Enter different title:");
                inputTitle = Console.ReadLine();
            }

            Console.WriteLine("Select genere:");
            var generes = Enum.GetNames(typeof(Generes));
            for (int i = 0; i < generes.Length; i++)
            {
                Console.WriteLine($"{i+1}. {generes[i]}");
            }
            int selectedGenereId = 0;
            while(selectedGenereId > generes.Length || selectedGenereId < 1)
            {
                Console.WriteLine("Select: ");
                selectedGenereId = int.Parse(Console.ReadKey().KeyChar.ToString());
            }
            Generes selectedGenere = (Generes)selectedGenereId; 

            Console.WriteLine("Enter movie relase date:");
            int inputRelaseDate = 0;
            while (inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
            {
                inputRelaseDate = int.Parse(Console.ReadKey().KeyChar.ToString()); ;
                if(inputRelaseDate > (int)DateTime.Today.Year || inputRelaseDate < 1895)
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
}
