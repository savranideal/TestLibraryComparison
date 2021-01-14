using FakeItEasy;
using System;
using TestLibrary;
using Xunit;

namespace Mocking.FakeItEasy
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
            
            var movieScore = A.Fake<IMovieScore>();
            A.CallTo(() => movieScore.Score(A<string>.Ignored)).Returns(score);
            var movieSuggestion = new MovieSuggestion(movieScore);
            var title = Guid.NewGuid().ToString();

            
            var isGood = movieSuggestion.IsGoodMovie(title);

            
            Assert.Equal(expected,isGood);
            A.CallTo(() => movieScore.Score(title)).MustHaveHappened();
        }
    }
}
