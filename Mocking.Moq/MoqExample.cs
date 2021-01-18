using Moq;
using System;
using TestLibrary;
using Xunit;

namespace Mocking.XUnit
{


    public class MoqExample
    {

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 7.99)]
        [InlineData(true, 8)]
        [InlineData(true, 10)]
        public void CanSuggest(bool expected, double score)
        {
            
            var movieScore = new Mock<IMovieScore>();
            movieScore.Setup(ms => ms.Score(It.IsAny<string>())).Returns(score);
            var movieSuggestion = new MovieSuggestion(movieScore.Object);
            var title = Guid.NewGuid().ToString();
            var isGood = movieSuggestion.IsGoodMovie(title);

            
            Assert.Equal(expected,isGood);
            movieScore.Verify(ms => ms.Score(title));
        }

    }
}
