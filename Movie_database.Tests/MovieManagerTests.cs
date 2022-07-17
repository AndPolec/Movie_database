using Moq;
using Movie_database.App.Abstract;
using Movie_database.App.Concrete;
using Movie_database.App.Managers;
using Movie_database.Domain.Common;
using Movie_database.Domain.Entity;
using System;
using System.Linq;
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
            var manager = new MovieManager(mock.Object,new UserInput(), new DisplayMovie(mock.Object));

            var actual = manager.IsMovieInDB(movie);

            Assert.True(actual);
        }

        [Fact]
        public void IsMovieInDB_MovieIsNotInDB_ShouldReturnFalse()
        {
            var movie = "Test Movie";
            var mock = new Mock<MovieService>();
            var manager = new MovieManager(mock.Object, new UserInput(), new DisplayMovie(mock.Object));

            var actual = manager.IsMovieInDB(movie);

            Assert.False(actual);
        }

        [Fact]
        public void AddMovie_MovieIsNotInDB_ShouldAdd()
        {
            var expected = new Movie(1, "Test", Generes.Comedy, 10, 2000);
            var movieServiceMock = new Mock<MovieService>();

            var userInputMock = new Mock<IUserInput>();
            userInputMock.Setup(m => m.SelectTitle()).Returns("Test");
            userInputMock.Setup(m => m.SelectGenere(Enum.GetNames(typeof(Generes)))).Returns(Generes.Comedy);
            userInputMock.Setup(m => m.SelectRating()).Returns(10);
            userInputMock.Setup(m => m.SelectRelaseDate()).Returns(2000);

            var manager = new MovieManager(movieServiceMock.Object,userInputMock.Object, new DisplayMovie(movieServiceMock.Object));
            manager.AddMovie();

            Assert.NotNull(movieServiceMock.Object.Items);
            Assert.True(movieServiceMock.Object.Items.Count == 1);
            Assert.Equal(expected.Id,movieServiceMock.Object.Items.First().Id);
            Assert.Equal(expected.Title,movieServiceMock.Object.Items.First().Title);
            Assert.Equal(expected.Genere,movieServiceMock.Object.Items.First().Genere);
            Assert.Equal(expected.YourRating,movieServiceMock.Object.Items.First().YourRating);
            Assert.Equal(expected.RelaseYear,movieServiceMock.Object.Items.First().RelaseYear);
        }

        [Fact]
        public void AddMovie_MovieIsInDB_ShouldNotAdd() 
        {
            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(new Movie(1, "Test", Generes.Comedy, 10, 2000));

            var userInputMock = new Mock<IUserInput>();
            userInputMock.Setup(m => m.SelectTitle()).Returns("Test");

            var manager = new MovieManager(movieServiceMock.Object, userInputMock.Object, new DisplayMovie(movieServiceMock.Object));
            manager.AddMovie();

            Assert.NotNull(movieServiceMock.Object.Items);
            Assert.True(movieServiceMock.Object.Items.Count == 1);
            
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void EditMovie_CorrectInput_ShouldEdit(int whatToEdit)
        {
            var expected = new Movie(1, "Title", Generes.Comedy, 1, 1990);
            var actual = new Movie(1, "Test", Generes.Action, 10, 2001);
            var menuList = new MenuService().GetMenuByMenuName("Edit movie");

            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(actual);

            var userInputMock = new Mock<IUserInput>();
            userInputMock.SetupSequence(m => m.SelectTitle())
                .Returns(actual.Title)
                .Returns(expected.Title);
            userInputMock.Setup(m => m.SelectMenuOption(menuList)).Returns(whatToEdit);
            userInputMock.Setup(m => m.SelectGenere(Enum.GetNames(typeof(Generes)))).Returns(expected.Genere);
            userInputMock.Setup(m => m.SelectRating()).Returns(expected.YourRating);
            userInputMock.Setup(m => m.SelectRelaseDate()).Returns(expected.RelaseYear);

            var manager = new MovieManager(movieServiceMock.Object, userInputMock.Object, new DisplayMovie(movieServiceMock.Object));
            manager.EditMovie(menuList);

            Assert.NotNull(movieServiceMock.Object.Items);
            Assert.True(movieServiceMock.Object.Items.Count == 1);

            if (whatToEdit == 1)
                Assert.Equal(expected.Title, movieServiceMock.Object.Items[0].Title);

            if (whatToEdit == 2)
                Assert.Equal(expected.Genere, movieServiceMock.Object.Items[0].Genere);

            if (whatToEdit == 3)
                Assert.Equal(expected.YourRating, movieServiceMock.Object.Items[0].YourRating);

            if (whatToEdit == 4)
                Assert.Equal(expected.RelaseYear, movieServiceMock.Object.Items[0].RelaseYear);
        }

        [Fact]
        public void DeleteMovie_FoundMovieInDB_ShouldDelete()
        {
            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(new Movie(1, "Test", Generes.Comedy, 10, 2000));

            var userInputMock = new Mock<IUserInput>();
            userInputMock.Setup(m => m.SelectTitle()).Returns("Test");

            var manager = new MovieManager(movieServiceMock.Object, userInputMock.Object, new DisplayMovie(movieServiceMock.Object));
            manager.DeleteMovie();

            Assert.True(movieServiceMock.Object.Items.Count == 0);
        }

        [Fact]
        public void DeleteMovie_MovieNotFoundInDB_ShouldNotDelete()
        {
            var movie = new Movie(1, "Test", Generes.Comedy, 10, 2000);
            var movieServiceMock = new Mock<MovieService>();
            movieServiceMock.Object.Add(movie);

            var userInputMock = new Mock<IUserInput>();
            userInputMock.Setup(m => m.SelectTitle()).Returns("NotInDb");

            var manager = new MovieManager(movieServiceMock.Object, userInputMock.Object, new DisplayMovie(movieServiceMock.Object));
            manager.DeleteMovie();

            Assert.True(movieServiceMock.Object.Items.Count == 1);
            Assert.Contains(movie, movieServiceMock.Object.Items);
        }

        [Fact]
        public void DisplayRanking_ShouldActivateProperDisplayMethod()
        {
            var movieServiceMock = new Mock<MovieService>();
            var displayMock = new Mock<IDisplayMovie>();
            var userInputMock = new Mock<IUserInput>();

            var manager = new MovieManager(movieServiceMock.Object, userInputMock.Object, displayMock.Object);
            manager.DisplayRanking();

            displayMock.Verify(m => m.Ranking(), Times.Once());

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DisplayMovie_ShouldActivateProperDisplayMethods(int optionToDisplay)
        {
            Generes genereName = Generes.Comedy;
            int relaseYear = 2000;
            var menuList = new MenuService().GetMenuByMenuName("Display movies from DB");

            var movieServiceMock = new Mock<MovieService>();
            var displayMock = new Mock<IDisplayMovie>();
            
            var userInputMock = new Mock<IUserInput>();
            userInputMock.Setup(m => m.SelectMenuOption(menuList)).Returns(optionToDisplay);
            userInputMock.Setup(m => m.SelectGenere(Enum.GetNames(typeof(Generes)))).Returns(genereName);
            userInputMock.Setup(m => m.SelectRelaseDate()).Returns(relaseYear);

            var manager = new MovieManager(movieServiceMock.Object, userInputMock.Object, displayMock.Object);
            manager.DisplayMovies(menuList);

            if(optionToDisplay == 1)
                displayMock.Verify(m => m.AllMovies(), Times.Once);

            if (optionToDisplay == 2)
                displayMock.Verify(m => m.MoviesByGenere(genereName), Times.Once);

            if (optionToDisplay == 3)
                displayMock.Verify(m => m.MoviesByRelaseYear(relaseYear), Times.Once);

        }
    
    }
}
