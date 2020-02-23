# Construction Line code challenge

## Notes on logic
As it is not entirely clear from the description of the challenge on the exact logic that is required for the search engine implementation, some assumptions have been made based on how facted search engines typically work.  
  
1. In the search options there are currently only 2 facets - color and size (but these could be expanded later)  
2. It is optional to supply selections for a facet  
3. If no selections are provided for a facet it is assumed that ALL selections for that facet will be returned  
4. The selections within a facet are ORED together (e.g. if you select both red and black in the color facet, both red and black shirts will be returned)  
5. The selections across facets are ANDED together. For example, if you pick red for the color facet and small for the size facet, only shirts that match both red AND small will be returned)  
6. When suppling multiple selections in each facet the above rules will still apply (e.g. if you select red and black for the color facet and small and medium for the size facet you will get all red shirts that are small or medium and all black shirts that are small or medium)  

## Change to SearchEngineTestsBase
The assert methods supplied in the SerchEngineTestsBase class (AssertSizeCounts and AssetColorCounts) both assume that the choices across facets will be ORED (e.g. if you select red and medium all red shirts as well as all medium shirts will be returned). This is not the desired behaviour based on the above assumptions and therefore I have adapted these methods to ensure they take the logic prescribed above into condsideration.  

If we wanted to update the logic prescribed above to actually OR the facets together, this would need to be updated in these Assert methods as well as in the PerformSearch logic in the Search Engines.

## Performance Results
| Records       | LINQ     | PLINQ    | Keyed
| ------------- |:--------:|:--------:| --------:
| 50000         | 11ms     | 12ms     | 8ms 
| 500000        | 133ms    | 95ms     | 32ms
| 5000000       | 656ms    | 416ms    | 245ms 
| 50000000      | 6348ms   | 4288ms   | 2289ms 

## Further Performance Enhancements
If we look at performance further, we can see that 5000000 using the Keyed search engine we have 245ms of total overhead, yet ~230ms of this is caused by the LINQ group by statements in the FacetCounter class. If further performance optimisation is required this would be the firt place to start looking.  