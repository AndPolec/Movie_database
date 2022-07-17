using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Abstract
{
    public interface IUserInput
    {
        int SelectMenuOption(List<Menu> editMenu);
        Generes SelectGenere(string[] generes);
        int SelectRelaseDate();
        int SelectRating();
        string SelectTitle();
    }
}
