using Movie_database.App.Concrete;
using Movie_database.App.Managers;

var movieService = new MovieService();
var userInput = new UserInput();
var display = new DisplayMovie(movieService);
var movieManager = new MovieManager(movieService, userInput, display);
var menuService = new MenuService();
var mainMenu = menuService.GetMenuByMenuName("Main");
bool quit = false;

Console.WriteLine("Welcome in movie database!");

while (!quit)
{
    Console.WriteLine("Select action:");
    var select = userInput.SelectMenuOption(mainMenu);

    switch (select)
    {
        case 1:
            movieManager.AddMovie();
            break;
        case 2:
            movieManager.EditMovie(menuService.GetMenuByMenuName("Edit movie"));
            break;
        case 3:
            movieManager.DeleteMovie();
            break;
        case 4:
            movieManager.DisplayMovies(menuService.GetMenuByMenuName("Display movies from DB"));
            break;
        case 5:
            movieManager.DisplayRanking();
            break;
        case 6:
            quit = true;
            break;
        default:
            Console.WriteLine("Selected action is unknown.");
            break;
    }

}



