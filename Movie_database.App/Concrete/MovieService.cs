using Movie_database.App.Common;
using Movie_database.Domain.Entity;
using Movie_database.Domain.Common;

namespace Movie_database.App.Concrete
{
    public class MovieService : BaseService<Movie>
    {
        public void Remove(string title)
        {
            Items.RemoveAll(x => x.Title.ToLower() == title.ToLower());
        }

    }
}
