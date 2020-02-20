using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngineKeyed : SearchEngineBase
    {
        private Dictionary<string, List<Shirt>> shirtLookup = new Dictionary<string, List<Shirt>>();

        public SearchEngineKeyed(List<Shirt> shirts) : base(shirts)
        {
            // Now that we have our search objects let's add them to
            // a dictionary keyed by their color and size ids
            foreach(var shirt in _shirts)
            {
                var key = shirt.Color.Id.ToString() + shirt.Size.Id.ToString();
                if(shirtLookup.ContainsKey(key))
                {
                    shirtLookup[key].Add(shirt);
                } 
                else
                {
                    shirtLookup.Add(key, new List<Shirt> { shirt });
                }
            }
        }

        /// <summary>
        /// Perform search using a PLINQ query
        /// </summary>
        /// <param name="options">A set of search query options</param>
        /// <returns>A filtered list of shirts that match the search query options</returns>
        protected override List<Shirt> PerformSearch(SearchOptions options)
        {
            var searchKeys = new List<string>();
            foreach (var color in options.Colors)
            {
                searchKeys.AddRange(options.Sizes
                    .Select(size => color.Id.ToString() + size.Id.ToString())
                );
            }

            return shirtLookup
                .Where(shirt => searchKeys.Contains(shirt.Key))
                .SelectMany(shirt => shirt.Value)
                .ToList();
        }
    }
}
