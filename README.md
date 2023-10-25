
# Time Complexity

>A console application that runs different algorithms against a custom size dataset and times them.


## Tech Stack

**Console Application:** C# 10.0, .NET 7.0

**Other technologies:** Spectre.Console 0.47.0, Spectre.Console.Cli 0.47.0

## Algorithms implemented
| **Type** | **Algorithm** | **Time Complexity** |
|:---:|:---:|:---:|
| **Sorted Search** | Basic For Loop | O(n) |
|  | Binary Search (Recursive) | O(log n) |
|  | Binary Search (Iterative) | O(log n) |
|  | Binary Search (Ubiquitous) | O(log n) |
|  | Exponential Search | O(log n) |
|  | Fibonacci Search | O(log n) |
|  | IndexOf() | O(n) |
|  | Interpolation Search | O(log log n) |
|  | Jump Search | O(√n) |
|  | Linear Search | O(n) |
|  | Ternary Search | O(log₃(n)) |
|  |  |  |
| **Unsorted Search** | Front and Back Search | O(n/2) |
|  | Linear Search | O(n) |
|  | Parallel Search | O(n/p) |
|  |  |  |
| **First Positive Search** | Binary Search (Unbound) | O(log n) |
|  | Linear Search | O(n) |
|  |  |  |
| **Hash Based Search** | Convert Array to Hash Table | O(n) |
|  | Hash Search | O(1) |
|  |  |  |
| **Sublist Search** | Basic | O(m * n) |
|  | Advanced | O(m + n) |
|  |  |  |
| **Sorting** | Bubble Sort | O(n^2) |
|  | Bidirectional Bubble Sort | O(n^2) |
|  | Bucket Sort | O(n^2) |
|  | Comb Sort | O(n^2) |
|  | Counting Sort | O(n + k) |
|  | Heap Sort | O(n * log(n)) |
|  | Insertion Sort | O(n^2) |
|  | Merge Sort | O(n * log(n)) |
|  | Odd-Even Sort | O(n^2) |
|  | Quick Sort | O(n^2) |
|  | Radix Sort | O(n * k) |
|  | Selection Sort | O(n^2) |
|  | Shell Sort | O(n^(4/3)) |

## Running Tests

To run tests, run the following command

#### Run All Tests
```bash
TimeComplexity.exe --all
```

#### Run Specific Tests

```bash
TimeComplexity.exe --search
```

```bash
TimeComplexity.exe --sort
```

```bash
TimeComplexity.exe --path
```

#### Specify a Custom Dataset Size

```bash
TimeComplexity.exe --all --datasetsize 5000000
```
![Algorithm test results](https://imgur.com/a/PTmN2gT)

## Acknowledgements
 - [Spectre.Console](https://spectreconsole.net/)
 - [Readme.so](https://readme.so/)


## Lessons Learned

#### 1. Algorithm Optimization
Making (some of the time) and comparing various searching, sorting and pathfinding algorithms was very insightful. I got a better understanding of their characteristics and efficiency, and it became evident that choosing the right algorithm for a specific task is crucial for optimizing runtime.

#### 2. Benchmarking and Profiling
Profiling and benchmarking code was something I was not all too familiar with, but they were essential for accurately measuring the performance of these algorithms.

#### 3. Time Complexity Analysis
First understanding what time complexity even is, and then having learned what logarithms were, i could use these for a deeper understanding of the algorithms behaviour. It emphasized the importance of selecting the right tool for the job to avoid unnecessary performance overhead.

#### 4. Data Presentation
Displaying results in the console in an organized way was a challenge until I discovered Spectre.Console.


#### 5. Documentation
Effective documentation of code was crucial for maintaining an understanding of the different algorithms. Without them, remembering how they work would be a challenge.
## License

[MIT](https://choosealicense.com/licenses/mit/)

