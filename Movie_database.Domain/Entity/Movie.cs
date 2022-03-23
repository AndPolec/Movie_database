using Movie_database.Domain.Common;


namespace Movie_database.Domain.Entity
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public  Generes Genere { get; set; }
        public int YourRating { get; set; }
        public int RelaseYear { get; set; }
        public DateTime AdditionDate { get; set; }

        public Movie(int id, string title, Generes genereId, int yourRating, int relaseYear)
        {
            Id = id;
            Title = title;
            Genere = genereId;
            YourRating = yourRating;
            RelaseYear = relaseYear;
            AdditionDate = DateTime.Now;
        }
    }

   
}
