```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5189/23H2/2023Update/SunValley3)
Intel Core Ultra 7 165H, 1 CPU, 22 logical and 16 physical cores
.NET SDK 9.0.203
  [Host]     : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2
  Job-BBDABY : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2

InvocationCount=1  IterationCount=50  RunStrategy=Monitoring  
UnrollFactor=1  

```
| Method               | Empty | Mean       | Error       | StdDev      | Median     | Min        | Max          |
|--------------------- |------ |-----------:|------------:|------------:|-----------:|-----------:|-------------:|
| **AddOperation_10**      | **False** | **4,448.4 ns** | **4,718.60 ns** |  **9,531.8 ns** | **2,755.0 ns** | **1,960.0 ns** |  **69,760.0 ns** |
| AddOperation_100     | False | 1,633.9 ns |   725.15 ns |  1,464.8 ns | 1,402.0 ns |   773.0 ns |  11,538.0 ns |
| AddOperation_1000    | False | 1,309.1 ns |   528.09 ns |  1,066.8 ns | 1,007.2 ns |   776.9 ns |   6,864.5 ns |
| AddOperation_10_000  | False | 1,151.8 ns |   196.42 ns |    396.8 ns | 1,006.2 ns |   689.5 ns |   2,192.9 ns |
| AddOperation_100_000 | False |   940.6 ns |    81.06 ns |    163.8 ns |   921.4 ns |   717.4 ns |   1,411.2 ns |
| **AddOperation_10**      | **True**  | **4,801.4 ns** | **9,465.41 ns** | **19,120.6 ns** | **2,100.0 ns** | **1,510.0 ns** | **137,270.0 ns** |
| AddOperation_100     | True  | 1,322.5 ns |   782.71 ns |  1,581.1 ns | 1,126.0 ns |   540.0 ns |  11,726.0 ns |
| AddOperation_1000    | True  | 1,091.3 ns |   747.33 ns |  1,509.6 ns |   734.4 ns |   494.0 ns |  10,855.9 ns |
| AddOperation_10_000  | True  |   896.7 ns |   126.68 ns |    255.9 ns |   879.5 ns |   539.2 ns |   2,087.5 ns |
| AddOperation_100_000 | True  | 1,282.1 ns |   140.33 ns |    283.5 ns | 1,278.9 ns |   884.0 ns |   1,931.9 ns |
