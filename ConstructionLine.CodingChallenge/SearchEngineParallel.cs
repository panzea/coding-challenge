using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngineParallel : SearchEngineBase
    {
        public SearchEngineParallel(List<Shirt> shirts) : base(shirts)
        { }

        /// <summary>
        /// Perform search using a PLINQ query
        /// </summary>
        /// <param name="options">A set of search query options</param>
        /// <returns>A filtered list of shirts that match the search query options</returns>
        protected override List<Shirt> PerformSearch(SearchOptions options)
        {
            // Setup search. As the performance test indicates that you can
            // perform a search just for color, without specifying a size, it is
            // assumed that you can also search for a size without a color.
            return _shirts
                .AsParallel()
                .Where(shirt => (!options.Colors.Any() || options.Colors.Any(color => color == shirt.Color))
                    && !options.Sizes.Any() || (options.Sizes.Any(size => size == shirt.Size)))
                .ToList();
        }
    }
}
