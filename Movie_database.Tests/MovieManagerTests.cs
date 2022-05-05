using FluentAssertions;
using Moq;
using Movie_database.App.Concrete;
using Movie_database.App.Managers;
using Movie_database.Domain.Entity;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Movie_database.Tests
{
    public class MovieManagerTests
    {
        [Fact]
        public void IsMovieInDBTest()
        {
            Movie testMovie = new Movie(1, "Movie", Domain.Common.Generes.Comedy, 1, 1990);
            var mock = new Mock<MovieService>();
            mock.Object.Add(testMovie);
            var manager = new MovieManager(mock.Object);

            var result = manager.IsMovieInDB("Movie");

            result.Should().BeTrue();
        }

        [Fact]
        public void AddMovieTest()
        {
            //Arrange
            var mock = new Mock<MovieService>();
            var manager = new MovieManager(mock.Object);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Tytuł");
            stringBuilder.AppendLine("1");
            stringBuilder.AppendLine("1990");
            stringBuilder.AppendLine("10");

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            //Act
            manager.AddMovie();
            var result = mock.Object.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(1);
            result[0].Should().BeOfType<Movie>();
            result[0].Title.Should().Be("Tytuł");
            result[0].Genere.Should().Be(Domain.Common.Generes.Comedy);
            result[0].RelaseYear.Should().Be(1990);
            result[0].YourRating.Should().Be(10);
        }

        [Fact]
        public void EditMovieTest()
        {
            var mockMovieService = new Mock<MovieService>();
            var mockMenuService = new Mock<MenuService>();
            var editMenu = mockMenuService.Object.GetMenuByMenuName("Edit movie");
            mockMovieService.Object.Add(new Movie(1, "Tytuł", Domain.Common.Generes.Comedy, 1, 1990));
            var manager = new MovieManager(mockMovieService.Object);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Tytuł");
            stringBuilder.AppendLine("1");
            stringBuilder.AppendLine("Zmiana");
            stringBuilder.AppendLine("Zmiana");
            stringBuilder.AppendLine("2");
            stringBuilder.AppendLine("2");
            stringBuilder.AppendLine("Zmiana");
            stringBuilder.AppendLine("3");
            stringBuilder.AppendLine("10");
            stringBuilder.AppendLine("Zmiana");
            stringBuilder.AppendLine("4");
            stringBuilder.AppendLine("2000");

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            manager.EditMovie(editMenu);
            manager.EditMovie(editMenu);
            manager.EditMovie(editMenu);
            manager.EditMovie(editMenu);
           
            var result = mockMovieService.Object.Items[0];

            result.Title.Should().Be("Zmiana");
            result.Genere.Should().Be(Domain.Common.Generes.Action);
            result.YourRating.Should().Be(10);
            result.RelaseYear.Should().Be(2000);
        }
    }


}
