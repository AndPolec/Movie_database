//Movie Database
////1.Dodaj nowy film
////2.Edytuj istniejący film
////3.Usuń film
////4.Wyświetl filmy dostępne w bazie
////5.Ranking filmów
//////1a. Pobierz dane, i sprawdź czy film jest już w bazie
//////2a. Pobierz nazwę i sprawdź czy film jest w baze, jeśli tak  wyświetl dany i zapytaj co edytować 
//////3a. Pobierz nazwę i sprawdż czy istnieje, wyświetl dane i zapytaj czy na pewno usunąć
//////4a. Zapytaj czy wyświetlić wszystkie czy po gatunku
//////5a. Wyświetl ranking po ocenie

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
            break;
        case '4':
            break;
        case '5':
            break;
        case '6':
            quit = true;
            break;


        default:
            Console.WriteLine("Selected action is unknown.");
            break;
    }

}



