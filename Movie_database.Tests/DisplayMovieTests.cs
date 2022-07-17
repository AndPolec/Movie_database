using Moq;
using Movie_database.App.Concrete;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;
using System;
using System.IO;
using Xunit;

namespace Movie_database.Tests
{
    public class DisplayMovieTests
    {
        [Fact]
        public void MovieInfo_ShouldDisplayInfo()
        {
            string[] expected = {"*******************", "Title: Test Movie", "Genere: Comedy",
                                "Rating: 1/10", "Relase year: 2000", "*******************" };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var mock = new Mock<MovieService>();
            mock.Object.Items.Add(new Movie(1, "Test Movie", Domain.Common.Generes.Comedy, 1, 2000));
            var display = new DisplayMovie(mock.Object);

            display.MovieInfo("Test Movie");

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void DisplayRanking_ShouldDisplayCorrectly()
        {
            string[] expected = { "1.Title: Test2, Rating: 9", "2.Title: Test3, Rating: 5", "3.Title: Test1, Rating: 3" };


            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(new Movie(1, "Test3", Generes.Comedy, 5, 2000));
            movieServiceMock.Object.Add(new Movie(2, "Test2", Generes.Comedy, 9, 2000));
            movieServiceMock.Object.Add(new Movie(3, "Test1", Generes.Comedy, 3, 2000));

            var display = new DisplayMovie(movieServiceMock.Object);
            display.Ranking();

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void AllMovies_ShouldDisplayCorrectly()
        {
            string[] expected = { "*******************", "Title: Test2", "Genere: Comedy",
                                "Rating: 9/10", "Relase year: 2000", "*******************",
                                "*******************", "Title: Test3", "Genere: Comedy",
                                "Rating: 5/10", "Relase year: 2000", "*******************"};


            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(new Movie(1, "Test3", Generes.Comedy, 5, 2000));
            movieServiceMock.Object.Add(new Movie(2, "Test2", Generes.Comedy, 9, 2000));

            var display = new DisplayMovie(movieServiceMock.Object);
            display.AllMovies();

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MoviesByGenere_NoMovieWithGenereInDb_ShouldDisplayNoMovieInfo()
        {
            string[] expected = { "There are no movies from the selected genere in the database." };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var movieServiceMock = new Mock<MovieService>();

            var display = new DisplayMovie(movieServiceMock.Object);
            display.MoviesByGenere(Generes.Comedy);

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MoviesByGenere_MovieWithGenereInDb_ShouldDisplayMovieInfo()
        {
            string[] expected = { "*******************", "Title: Test3", "Genere: Comedy",
                                "Rating: 5/10", "Relase year: 2000", "*******************",
                                "*******************", "Title: Test2", "Genere: Comedy",
                                "Rating: 9/10", "Relase year: 2000", "*******************"};

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(new Movie(1, "Test3", Generes.Comedy, 5, 2000));
            movieServiceMock.Object.Add(new Movie(2, "Test2", Generes.Comedy, 9, 2000));

            var display = new DisplayMovie(movieServiceMock.Object);
            display.MoviesByGenere(Generes.Comedy);

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MoviesByRelaseYear_NoMovieWithSelectedYearInDb_ShouldDisplayNoMovieInfo()
        {

            string[] expected = { "There are no movies from the selected year in the database." };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var movieServiceMock = new Mock<MovieService>();

            var display = new DisplayMovie(movieServiceMock.Object);
            display.MoviesByRelaseYear(1992);

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);
            
        }

        [Fact]
        public void MoviesByRelaseYear_MovieWithSelectedYearInDb_ShouldDisplayMovieInfo()
        {

            string[] expected = { "*******************", "Title: Test3", "Genere: Comedy",
                                "Rating: 5/10", "Relase year: 2000", "*******************",
                                "*******************", "Title: Test2", "Genere: Comedy",
                                "Rating: 9/10", "Relase year: 2000", "*******************"};

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(new Movie(1, "Test3", Generes.Comedy, 5, 2000));
            movieServiceMock.Object.Add(new Movie(2, "Test2", Generes.Comedy, 9, 2000));

            var display = new DisplayMovie(movieServiceMock.Object);
            display.MoviesByRelaseYear(2000);

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);

        }
    }
}
