using NSubstitute;
using System;
using TestLibrary;
using Xunit;

namespace Mocking.NSubstitute
{

    public class MoqExample
    {

        [Theory]
        [InlineData(false, 0)]
        [InlineData(false, 7.99)]
        [InlineData(true, 8)]
        [InlineData(true, 10)]
        public void CanSuggest(bool expected, double score)
        {
            
            var movieScore = Substitute.For<IMovieScore>();
            movieScore.Score(Arg.Any<string>()).Returns(score); 
            var movieSuggestion = new MovieSuggestion(movieScore);
            var title = Guid.NewGuid().ToString();

            
            var isGood = movieSuggestion.IsGoodMovie(title);

            
            Assert.Equal(expected, isGood);
            movieScore.When(ms => ms.Score(title));
        }

    }
}
