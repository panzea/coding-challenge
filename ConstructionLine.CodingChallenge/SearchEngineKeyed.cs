using System.Collections.Generic;
using System.Linq;
using ConstructionLine.CodingChallenge.Domain;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngineKeyed : SearchEngineBase
    {
        private readonly Dictionary<int, ColorSizeMap> Map;
        private readonly Dictionary<int, List<Shirt>> Index;

        public SearchEngineKeyed(List<Shirt> shirts) : base(shirts)
        {
            // First we will initialize a map that takes every color and 
            // size combo and assigns them an id
            Map = new Dictionary<int, ColorSizeMap>();
            var index = 0;

            Color.All.ForEach(color =>
            {
                Size.All.ForEach(size =>
                {
                    Map.Add(index, new ColorSizeMap(color, size));
                    index++;
                });
            });

            // Create an index of shirts that match the color/size
            // combo id
            Index = new Dictionary<int, List<Shirt>>();
            foreach (var colorSizeMap in Map)
            {
                var matchingShirts = _shirts
                        .Where(shirt => shirt.Color.Id == colorSizeMap.Value.Color.Id
                                && shirt.Size.Id == colorSizeMap.Value.Size.Id);
                Index.Add(colorSizeMap.Key, matchingShirts.ToList());
            }
        }

        /// <summary>
        /// Perform search using a PLINQ query
        /// </summary>
        /// <param name="options">A set of search query options</param>
        /// <returns>A filtered list of shirts that match the search query options</returns>
        protected override List<Shirt> PerformSearch(SearchOptions options)
        {
            var searchKeys = new List<int>();

            // Determine what options have been selected for our facets, and
            // if none have been selected for a particular facet, assume ALL
            // options have been selected for that facet
            var colors = options.Colors.Any() ? options.Colors : Color.All;
            var sizes = options.Sizes.Any() ? options.Sizes : Size.All;

            // Given our facet selections, determine the list of keys we need 
            // to retrieve shirts from
            colors.ForEach(color =>
            {
                sizes.ForEach(size =>
                {
                    var colorSizeMap = Map
                        .FirstOrDefault(m => m.Value.Color == color && m.Value.Size == size);
                    searchKeys.Add(colorSizeMap.Key);
                });
            });

            // Lookup the keys in our index and return the values
            var result = Index
                .Where(i => searchKeys.Contains(i.Key))
                .SelectMany(i => i.Value)
                .ToList();

            return result;
        }
    }
}
