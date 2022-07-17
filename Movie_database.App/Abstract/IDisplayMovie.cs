using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;

namespace Movie_database.App.Abstract
{
    public interface IDisplayMovie
    {

        IService<Movie> Service { get; }

        public void MovieInfo(string title);

        public void Ranking();

        public void AllMovies();

        public void MoviesByGenere(Generes genere);

        public void MoviesByRelaseYear(int selectedYear);
    }
}
