using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Domain;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineShould : SearchEngineTestsBase
    {
        [Test]
        public void OriginalTest()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoSearchOptionsProvided_ThrowAnArgumentException()
        {
            var shirts = AssumeSearchEngineWithResults();
            SearchOptions searchOptions = null;

            Assert.Throws<ArgumentException>(() =>
            {
                var results = _searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void GivenPerformingSearch_WhenSearchOptionsProvidedWithNullColor_ThrowAnArgumentException()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small },
                Colors = null
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var results = _searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void GivenPerformingSearch_WhenSearchOptionsProvidedWithNullSize_ThrowAnArgumentException()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = null
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var results = _searchEngine.Search(searchOptions);
            });
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsExist_ReturnNoResults()
        {
            var shirts = AssumeSearchEngineWithNoResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Small }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidSingleColorAndSizeProvided_ReturnOnlyResultsThatMatchBothTheColorAndSizeExactly()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Medium }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidMultipleColorsAndSingleSizeProvided_ReturnOnlyResultsThatMatchTheSizeAndOneOfTheColors()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Black },
                Sizes = new List<Size> { Size.Medium }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidSingleColorsAndMultipleSizesProvided_ReturnOnlyResultsThatMatchColorAndOneOfTheSizes()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black },
                Sizes = new List<Size> { Size.Medium, Size.Large }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenValidMultipleColorsAndMultipleSizesProvided_ReturnOnlyResultsThatMatchOneOfTheColorsAndOneOfTheSizes()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Black, Color.Blue },
                Sizes = new List<Size> { Size.Medium, Size.Large }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsWithSelectedColorExists_ReturnNoResults()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Medium }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsWithSelectedColorAndSizeExists_ReturnNoResults()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Yellow },
                Sizes = new List<Size> { Size.Small }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenNoShirtsWithSelectedSizeExists_ReturnNoResults()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenSingleColorAndNoSizeProvided_ReturnAllResultsThatMatchColor()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenPerformingSearch_WhenSingleSizeAndNoColorProvided_ReturnAllResultsThatMatchSize()
        {
            var shirts = AssumeSearchEngineWithResults();
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Small }
            };

            var results = _searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }
    }
}
