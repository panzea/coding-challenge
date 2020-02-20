using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngineBase : ISearchEngine
    {
        protected readonly List<Shirt> _shirts;

        public SearchEngineBase(List<Shirt> shirts)
        {
            if (shirts == null)
            {
                throw new ArgumentException("You must provide a valid set of shirts to use the search engine", nameof(shirts));
            }

            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options == null || options.Colors == null || options.Sizes == null)
            {
                throw new ArgumentException("You must provide valid search options when searching", nameof(options));
            }

            // Execute Search
            var shirtsList = PerformSearch(options);

            // Execute Color count
            var colorCountsList = CountColors(shirtsList);

            // Execute Size count
            var sizeCountsList = CountSizes(shirtsList);

            return new SearchResults
            {
                Shirts = shirtsList,
                ColorCounts = colorCountsList,
                SizeCounts = sizeCountsList
            };
        }

        /// <summary>
        /// The search implementation
        /// </summary>
        /// <param name="options">A set of search query options</param>
        /// <returns>A filtered list of shirts that match the search query options</returns>
        protected virtual List<Shirt> PerformSearch(SearchOptions options)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determine the aggregate counts for all shirt colors in the resultset
        /// </summary>
        /// <param name="shirts">List of filtered shirts from search</param>
        /// <returns>List of aggregate counts of each shirt color</returns>
        protected virtual List<ColorCount> CountColors(List<Shirt> shirts)
        {
            var result = shirts.GroupBy(shirt => shirt.Color)
                .Select(s => new ColorCount
                {
                    Color = s.Key,
                    Count = s.Count()
                }).ToList();

            var emptyCounts = Color.All.Where(c => !result.Any(s => s.Color == c))
                .Select(s => new ColorCount
                {
                    Color = s,
                    Count = 0
                });

            result.AddRange(emptyCounts);
            return result;
        }

        /// <summary>
        /// Determine the aggregate counts for all shirt sizes in the resultset
        /// </summary>
        /// <param name="shirts">List of filtered shirts from search</param>
        /// <returns>List of aggregate counts of each shirt size</returns>
        protected virtual List<SizeCount> CountSizes(List<Shirt> shirts)
        {
            var result = shirts.GroupBy(shirt => shirt.Size)
                .Select(s => new SizeCount
                {
                    Size = s.Key,
                    Count = s.Count()
                }).ToList();

            var emptyCounts = Size.All.Where(c => !result.Any(s => s.Size == c))
                .Select(s => new SizeCount
                {
                    Size = s,
                    Count = 0
                });

            result.AddRange(emptyCounts);
            return result;
        }
    }
}
