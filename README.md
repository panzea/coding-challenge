# Construction Line code challenge

## Performance Results
50000 Items = 25ms  
500000 Items = 123ms  
5000000 Items = 742ms  

| Records       | LINQ     | PLINQ    | Keyed
| ------------- |:--------:|:--------:| --------:
| 50000         | 22ms     | 17ms     | 6ms  
| 500000        | 165ms    | 125ms    | 6ms 
| 5000000       | 846ms    | 693ms    | 5ms 
| 50000000      | 9096ms   | 7247ms   | 5ms 