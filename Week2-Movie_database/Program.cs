using Movie_database.App.Concrete;
using Movie_database.App.Managers;

var movieManager = new MovieManager();
var menuService = new MenuService();
var mainMenu = menuService.GetMenuByMenuName("Main");
bool quit = false;

Console.WriteLine("Welcome in movie database!");

while (!quit)
{
    Console.WriteLine("Select action:");
    foreach (var menu in mainMenu)
    {
        Console.WriteLine($"{menu.Id}. {menu.MenuAction}");
    }

    var select = Console.ReadKey();

    switch (select.KeyChar)
    {
        case '1':
            movieManager.AddMovie();
            break;
        case '2':
            movieManager.EditMovie(menuService.GetMenuByMenuName("Edit movie"));
            break;
        case '3':
            movieManager.DeleteMovie();
            break;
        case '4':
            movieManager.DisplayAllMovies();
            break;
        case '5':
            movieManager.DisplayRanking();
            break;
        case '6':
            quit = true;
            break;
        default:
            Console.WriteLine("Selected action is unknown.");
            break;
    }

}



