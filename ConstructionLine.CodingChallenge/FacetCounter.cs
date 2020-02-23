using ConstructionLine.CodingChallenge.Abstractions;
using ConstructionLine.CodingChallenge.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class FacetCounter : IFacetCounter
    {
        /// <summary>
        /// Determine the aggregate counts for all shirt colors in the resultset
        /// </summary>
        /// <param name="shirts">List of filtered shirts from search</param>
        /// <returns>List of aggregate counts of each shirt color</returns>
        public virtual List<ColorCount> CountColors(List<Shirt> shirts)
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
        public virtual List<SizeCount> CountSizes(List<Shirt> shirts)
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
