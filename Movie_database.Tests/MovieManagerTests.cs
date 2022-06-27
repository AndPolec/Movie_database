using Autofac.Extras.Moq;
using Moq;
using Movie_database.App.Abstract;
using Movie_database.App.Concrete;
using Movie_database.App.Managers;
using Movie_database.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace Movie_database.Tests
{
    public class MovieManagerTests
    {
        [Fact]
        public void IsMovieInDB_MovieFromDB_ShouldReturnTrue()
        {
            var movie = "Test Movie";
            var mock = new Mock<MovieService>();
            mock.Object.Items.Add(new Movie(1,"Test Movie",Domain.Common.Generes.Comedy,1,2000));
            var manager = new MovieManager(mock.Object);

            var actual = manager.IsMovieInDB(movie);

            Assert.True(actual);
        }

        [Fact]
        public void IsMovieInDB_MovieIsNotInDB_ShouldReturnFalse()
        {
            var movie = "Test Movie";
            var mock = new Mock<MovieService>();
            var manager = new MovieManager(mock.Object);

            var actual = manager.IsMovieInDB(movie);

            Assert.False(actual);
        }

        [Fact]
        public void ShowMovieInfo_ShouldDisplayInfo()
        {
            string[] expected = {"*******************", "Title: Test Movie", "Genere: Comedy", 
                                "Rating: 1/10", "Relase year: 2000", "*******************" };

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var mock = new Mock<MovieService>();
            mock.Object.Items.Add(new Movie(1, "Test Movie", Domain.Common.Generes.Comedy, 1, 2000));
            var manager = new MovieManager(mock.Object);

            manager.ShowMovieInfo("Test Movie");

            var actual = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(expected, actual);
            
        }
    }
}
