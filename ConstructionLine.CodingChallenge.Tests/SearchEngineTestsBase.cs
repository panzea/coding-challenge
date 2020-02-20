using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        protected ISearchEngine _searchEngine;

        protected void AssumeSearchEngineWithNoResults()
        {
            var noShirts = new List<Shirt>();

            _searchEngine = new SearchEngine(noShirts);
        }

        protected void AssumeSearchEngineWithResults()
        {
            // GF: Not using SampleDataBuilder here so that we get a consistent set of results that
            // are predictable and will no change over the lifetime of the tests so that we can ensure
            // consistency of data and avoid scenarios where data might not be as we expect
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Black - Large", Size.Large, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            _searchEngine = new SearchEngine(shirts);
        }

        protected static void AssertResults(List<Shirt> shirts, SearchOptions options)
        {
            Assert.That(shirts, Is.Not.Null);

            var resultingShirtIds = shirts.Select(s => s.Id).ToList();
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in shirts)
            {
                if (sizeIds.Contains(shirt.Size.Id)
                    && colorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }


        protected static void AssertSizeCounts(List<Shirt> shirts, SearchOptions searchOptions, List<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in Size.All)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

                var expectedSizeCount = shirts
                    .Count(shirt => shirt.Size.Id == size.Id);

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount), 
                    $"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }


        protected static void AssertColorCounts(List<Shirt> shirts, SearchOptions searchOptions, List<ColorCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);
            
            foreach (var color in Color.All)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

                var expectedColorCount = shirts
                    .Count(shirt => shirt.Color.Id == color.Id);

                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }

        protected void AssertSearchEngineResults(SearchResults searchResults, SearchOptions searchOptions)
        {
            AssertResults(searchResults.Shirts, searchOptions);
            AssertSizeCounts(searchResults.Shirts, searchOptions, searchResults.SizeCounts);
            AssertColorCounts(searchResults.Shirts, searchOptions, searchResults.ColorCounts);
        }
    }
}