using NUnit.Framework;
using System;
using TestLibrary;

namespace UnitTest.NUnit
{
    [TestFixture]
   public class MovieSuggestionTest_Manual
    {
        [TestCase(false, 0)]
        [TestCase(false, 0)]
        [TestCase(false, 7.99)]
        [TestCase(true, 8)]
        [TestCase(true, 10)]
        public void CanSuggest(bool expected, double score)
        {
            
            var movieScore = new MovieScoreStub(score);
            var movieSuggestion = new MovieSuggestion(movieScore);
            var title = Guid.NewGuid().ToString();

            
            var isGood = movieSuggestion.IsGoodMovie(title);

            
            Assert.That(isGood, Is.EqualTo(expected));
        }

       
    }
}