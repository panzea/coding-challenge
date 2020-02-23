using ConstructionLine.CodingChallenge.Domain;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge.Abstractions
{
    public interface IFacetCounter
    {
        List<ColorCount> CountColors(List<Shirt> shirts);
        List<SizeCount> CountSizes(List<Shirt> shirts);
    }
}
