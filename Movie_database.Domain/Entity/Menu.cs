using Movie_database.Domain.Common;

namespace Movie_database.Domain.Entity
{
    public class Menu : BaseEntity
    {
        public string MenuAction { get; set; }
        public string MenuName { get; set; }

        public Menu(int id, string menuAction, string menuName)
        {
            Id = id;
            MenuAction = menuAction;
            MenuName = menuName;
        }
    }
}
