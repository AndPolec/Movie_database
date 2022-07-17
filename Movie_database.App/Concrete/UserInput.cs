using Movie_database.App.Abstract;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Concrete
{
    public class UserInput : IUserInput
    {
        public int SelectMenuOption(List<Menu> editMenu)
        {
            MenuService.DisplayMenu(editMenu);

            Console.WriteLine("Select:");

            int select = -1;

            while (select == -1)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                if (char.IsDigit(input.KeyChar) && int.Parse(input.KeyChar.ToString()) >= 1 && int.Parse(input.KeyChar.ToString()) <= editMenu.Count)
                    select = int.Parse(input.KeyChar.ToString());
            }

            return select;
        }

        public Generes SelectGenere(string[] generes)
        {
            for (int i = 0; i < generes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {generes[i]}");
            }

            int selectedGenereNumber = -1;
            Console.WriteLine("Select: ");

            while (selectedGenereNumber == -1)
            {
                ConsoleKeyInfo userInput = Console.ReadKey();

                if (char.IsDigit(userInput.KeyChar) && int.Parse(userInput.KeyChar.ToString()) >= 1 && int.Parse(userInput.KeyChar.ToString()) <= generes.Length)
                    selectedGenereNumber = int.Parse(userInput.KeyChar.ToString());
            }

            return (Generes)selectedGenereNumber;
        }

        public int SelectRelaseDate()
        {
            int inputRelaseDate = 0;
            while (inputRelaseDate > DateTime.Today.Year || inputRelaseDate < 1895)
            {
                inputRelaseDate = int.Parse(Console.ReadLine());
            }

            return inputRelaseDate;
        }

        public int SelectRating()
        {
            int rating = 0;
            while (rating > 11 || rating < 1)
            {
                rating = int.Parse(Console.ReadLine());
            }

            return rating;
        }

        public string SelectTitle()
        {
            string title = string.Empty;

            while (string.IsNullOrWhiteSpace(title))
            {
                title = Console.ReadLine();
            }

            return title;
        }

    }
}
