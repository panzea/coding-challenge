using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineShould : SearchEngineTestsBase
    {
        [Test]
        public void GivenPerformingSearch_WhenNoSearchOptionsProvided_ThrowAnArgumentException()
        {
            AssumeSearchEngineWithResults();
            SearchOptions searchOptions = null;

            Assert.Throws<ArgumentException>(() =>
            {
                var searchResults = _searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void GivenPerformingSearch_WhenSearchOptionsProvidedWithNoColor_ThrowAnArgumentException()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small },
                Colors = null
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var searchResults = _searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void GivenPerformingSearch_WhenSearchOptionsProvidedWithNoSize_ThrowAnArgumentException()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = null
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var searchResults = _searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsExist_ReturnNoResults()
        {
            AssumeSearchEngineWithNoResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Small }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidSingleColorAndSizeProvided_ReturnOnlyResultsThatMatchCriteria()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Medium }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidMultipleColorsAndSingleSizeProvided_ReturnOnlyResultsThatMatchCriteria()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black },
                Sizes = new List<Size> { Size.Medium }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidSingleColorsAndMultipleSizesProvided_ReturnOnlyResultsThatMatchCriteria()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size> { Size.Medium, Size.Large }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidMultipleColorsAndSizesProvided_ReturnOnlyResultsThatMatchCriteria()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black, Color.Blue },
                Sizes = new List<Size> { Size.Medium, Size.Large }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsWithSelectedColorExists_ReturnNoResults()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Medium }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsWithSelectedColorAndSizeExists_ReturnNoResults()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Small }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsWithSelectedSizeExists_ReturnNoResults()
        {
            AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var searchResults = _searchEngine.Search(searchOptions);

            AssertSearchEngineResults(searchResults, searchOptions);
        }
    }
}
