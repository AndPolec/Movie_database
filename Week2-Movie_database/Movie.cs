

namespace Movie_database
{
    public class Movie
    {
        public string Title { get; set; }
        public Generes Genere { get; set; }
        public int YourRating { get; set; }
        public int RelaseYear { get; set; }
        public DateTime AdditionDate { get; set; }

        public Movie(string title, Generes genereId, int yourRating, int relaseYear)
        {
            Title = title;
            Genere = genereId;
            YourRating = yourRating;
            RelaseYear = relaseYear;
            AdditionDate = DateTime.Now;
        }
    }
}
