```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.5189/23H2/2023Update/SunValley3)
Intel Core Ultra 7 165H, 1 CPU, 22 logical and 16 physical cores
.NET SDK 9.0.203
  [Host]     : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2
  Job-BEYIKH : .NET 8.0.15 (8.0.1525.16413), X64 RyuJIT AVX2

IterationCount=50  RunStrategy=Monitoring  

```
| Method              | Capacity | Mean     | Error    | StdDev    | Median   | Min      | Max        |
|-------------------- |--------- |---------:|---------:|----------:|---------:|---------:|-----------:|
| **GetElementOperation** | **10**       | **17.62 μs** | **38.74 μs** |  **78.25 μs** | **6.050 μs** | **3.300 μs** |   **559.6 μs** |
| **GetElementOperation** | **100**      | **19.36 μs** | **42.04 μs** |  **84.92 μs** | **6.450 μs** | **3.700 μs** |   **607.2 μs** |
| **GetElementOperation** | **1000**     | **18.15 μs** | **47.70 μs** |  **96.36 μs** | **4.100 μs** | **2.900 μs** |   **685.8 μs** |
| **GetElementOperation** | **10000**    | **28.84 μs** | **75.75 μs** | **153.02 μs** | **6.400 μs** | **3.000 μs** | **1,088.9 μs** |
