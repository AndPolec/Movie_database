namespace Movie_database
{
    public class MenuService
    {
        private List<Menu> menuList = new List<Menu>();

        public void AddMenu(int id, string menuAction, string menuName)
        {
            menuList.Add(new Menu(id, menuAction, menuName));
        }

        public List<Menu> GetMenuByMenuName(string menuName)
        {
            var menuListByMenuName = new List<Menu>();
            foreach(var menu in menuList)
            {
                if (menu.MenuName == menuName)
                    menuListByMenuName.Add(menu);
            }

            return menuListByMenuName;
        }


    }
}
