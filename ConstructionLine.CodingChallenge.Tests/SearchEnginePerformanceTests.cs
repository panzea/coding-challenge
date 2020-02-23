using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Domain;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;

        [SetUp]
        public void Setup()
        {
            var dataBuilder = new SampleDataBuilder(50000);

            _shirts = dataBuilder.CreateShirts();
        }


        [Test]
        public void SearchEnginePerformanceTest()
        {
            _searchEngine = new SearchEngine(_shirts);
            PerformanceTest("Search engine linq");
        }

        [Test]
        public void SearchEngineParallelPerformanceTest()
        {
            _searchEngine = new SearchEngineParallel(_shirts);
            PerformanceTest("Search engine plinq");
        }

        [Test]
        public void SearchEngineKeyedPerformanceTest()
        {
            _searchEngine = new SearchEngineKeyed(_shirts);
            PerformanceTest("Search engine keyed");
        }


        private void PerformanceTest(string testName)
        {
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = _searchEngine.Search(options);

            sw.Stop();
            Console.WriteLine($"{testName} finished in {sw.ElapsedMilliseconds} milliseconds");

            AssertResults(results.Shirts, options);
            AssertSizeCounts(_shirts, options, results.SizeCounts);
            AssertColorCounts(_shirts, options, results.ColorCounts);
        }
    }
}
