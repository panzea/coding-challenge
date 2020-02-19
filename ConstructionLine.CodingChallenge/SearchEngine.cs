using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options == null || options.Colors == null || options.Sizes == null)
            {
                throw new ArgumentException("You must provide valid search options when searching", "options");
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

        private List<Shirt> PerformSearch(SearchOptions options)
        {
            // Setup search. As the performance test indicates that you can
            // perform a search just for color, without specifying a size, it is
            // assumed that you can also search for a size without a color.
            return _shirts
                .Where(shirt => (!options.Colors.Any() || options.Colors.Any(color => color == shirt.Color))
                    && !options.Sizes.Any() || (options.Sizes.Any(size => size == shirt.Size)))
                .ToList();
        }

        private List<ColorCount> CountColors(List<Shirt> shirts)
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

        private List<SizeCount> CountSizes(List<Shirt> shirts)
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
