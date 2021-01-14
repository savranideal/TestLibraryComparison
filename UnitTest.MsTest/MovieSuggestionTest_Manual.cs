
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestLibrary;

namespace UnitTest.MsTest
{
    [TestClass]
    public class MovieSuggestionTest_Manual
    {
        [DataTestMethod]
        [DataRow(false, 0)]
        [DataRow(false, 7.99)]
        [DataRow(true, 8)]
        [DataRow(true, 10)]
        public void CanSuggest(bool expected, double score)
        {
            var movieScore = new MovieScoreStub(score);
            var movieSuggestion = new MovieSuggestion(movieScore);
            var title = Guid.NewGuid().ToString();

            
            var isGood = movieSuggestion.IsGoodMovie(title);

            
            Assert.AreEqual(expected, isGood);
        }


    }
}
