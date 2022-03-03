using Week2_Movie_database;

static void MenuInicialize(MenuService menuService)
{
    menuService.AddMenu(1, "Add new movie", "Main");
    menuService.AddMenu(2, "Edit movie", "Main");
    menuService.AddMenu(3, "Delete movie", "Main");
    menuService.AddMenu(4, "Display movies from DB", "Main");
    menuService.AddMenu(5, "Display rankings", "Main");
    menuService.AddMenu(6, "Quit", "Main");

    menuService.AddMenu(1, "Title", "Edit movie");
    menuService.AddMenu(2, "Genere", "Edit movie");
    menuService.AddMenu(3, "Rating", "Edit movie");
    menuService.AddMenu(4, "Relase date", "Edit movie");
}

MovieService movieService = new MovieService();
var menuService = new MenuService();
MenuInicialize(menuService);
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
            movieService.AddMovie();
            break;
        case '2':
            movieService.EditMovie(menuService);
            break;
        case '3':
            movieService.DeleteMovie();
            break;
        case '4':
            movieService.DisplayAllMovies();
            break;
        case '5':
            movieService.DisplayRanking();
            break;
        case '6':
            quit = true;
            break;
        default:
            Console.WriteLine("Selected action is unknown.");
            break;
    }

}



