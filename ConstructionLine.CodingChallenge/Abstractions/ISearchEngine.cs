using ConstructionLine.CodingChallenge.Domain;

namespace ConstructionLine.CodingChallenge.Abstractions
{
    public interface ISearchEngine
    {
        SearchResults Search(SearchOptions options);

    }
}
