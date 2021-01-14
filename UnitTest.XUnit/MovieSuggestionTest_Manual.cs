using System;
using TestLibrary;
using Xunit;

namespace UnitTest.XUnit
{
    public class MovieSuggestionTest_Manual:IDisposable
    {
       
        [Theory]
        [InlineData(false, 0)]
        [InlineData(false, 7.99)]
        [InlineData(true, 8)]
        [InlineData(true, 10)]
        public void CanSuggest(bool expected, double score)
        {
            
            var movieScore = new MovieScoreStub(score);
            var movieSuggestion = new MovieSuggestion(movieScore);
            var title = Guid.NewGuid().ToString();

            
            var isGood = movieSuggestion.IsGoodMovie(title);

            
            Assert.Equal(isGood, expected);
        }

        public void Dispose()
        {
            
        }
    }


}
