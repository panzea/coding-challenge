using ConstructionLine.CodingChallenge.Abstractions;
using ConstructionLine.CodingChallenge.Domain;
using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngineBase : ISearchEngine
    {
        protected readonly List<Shirt> _shirts;
        private IFacetCounter facetCounter;

        public SearchEngineBase(List<Shirt> shirts)
        {
            if (shirts == null)
            {
                throw new ArgumentException("You must provide a valid set of shirts to use the search engine", nameof(shirts));
            }

            _shirts = shirts;
            facetCounter = new FacetCounter();
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
            var colorCountsList = facetCounter.CountColors(shirtsList);

            // Execute Size count
            var sizeCountsList = facetCounter.CountSizes(shirtsList);

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
    }
}
