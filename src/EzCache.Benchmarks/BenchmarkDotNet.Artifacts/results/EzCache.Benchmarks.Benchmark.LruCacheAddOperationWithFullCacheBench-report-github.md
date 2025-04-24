```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5189/23H2/2023Update/SunValley3)
Intel Core Ultra 7 165H, 1 CPU, 22 logical and 16 physical cores
.NET SDK 9.0.203
  [Host]     : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2
  Job-OMXZBR : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2

InvocationCount=1  IterationCount=10  RunStrategy=Monitoring  
UnrollFactor=1  

```
| Method               | Mean       | Error       | StdDev      | Median     | Min      | Max         |
|--------------------- |-----------:|------------:|------------:|-----------:|---------:|------------:|
| AddOperation_10      | 8,634.0 ns | 34,918.4 ns | 23,096.4 ns | 1,250.0 ns | 970.0 ns | 74,360.0 ns |
| AddOperation_100     | 1,375.8 ns |  3,080.3 ns |  2,037.4 ns |   736.0 ns | 530.0 ns |  7,148.0 ns |
| AddOperation_1000    | 1,520.9 ns |  2,733.7 ns |  1,808.2 ns |   745.0 ns | 619.6 ns |  6,423.2 ns |
| AddOperation_10_000  |   870.9 ns |    692.8 ns |    458.3 ns |   724.0 ns | 538.9 ns |  2,112.3 ns |
| AddOperation_100_000 | 1,004.6 ns |    211.2 ns |    139.7 ns | 1,019.5 ns | 693.1 ns |  1,188.5 ns |
