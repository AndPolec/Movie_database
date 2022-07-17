using Movie_database.App.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Concrete;

public class MenuService : BaseService<Menu>
{

    public MenuService()
    {
        MenuInicialize();
    }

    private void MenuInicialize()
    {
        Add(new Menu(1, "Add new movie", "Main"));
        Add(new Menu(2, "Edit movie", "Main"));
        Add(new Menu(3, "Delete movie", "Main"));
        Add(new Menu(4, "Display movies from DB", "Main"));
        Add(new Menu(5, "Display ranking", "Main"));
        Add(new Menu(6, "Quit", "Main"));

        Add(new Menu(1, "Title", "Edit movie"));
        Add(new Menu(2, "Genere", "Edit movie"));
        Add(new Menu(3, "Rating", "Edit movie"));
        Add(new Menu(4, "Relase date", "Edit movie"));

        Add(new Menu(1, "Display all", "Display movies from DB"));
        Add(new Menu(2, "Display by genere", "Display movies from DB"));
        Add(new Menu(3, "Display by year", "Display movies from DB"));
        
    }
    public List<Menu> GetMenuByMenuName(string menuName)
    {
        var menuListByMenuName = new List<Menu>();
        foreach(var menu in Items)
        {
            if (menu.MenuName == menuName)
                menuListByMenuName.Add(menu);
        }

        return menuListByMenuName;
    }

    public static void DisplayMenu(List<Menu> menu)
    {
        foreach (var m in menu)
        {
            Console.WriteLine($"{m.Id}. {m.MenuAction}");
        }
    }

}
