using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_Movie_database
{
    public class Menu
    {
        public int Id { get; set; }
        public string MenuAction { get; set; }
        public string MenuName { get; set; }

        public Menu(int id, string menuAction, string menuName )
        {
            Id = id;
            MenuAction = menuAction;
            MenuName = menuName;
        }


    }
}
